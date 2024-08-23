using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.DTOs.AuthenUser;
using ApplicationCore.Entities.Common;
using ApplicationCore.Services.Common;
using ApplicationCore.Specifications.AuthenUser;
using ApplicationCore.Specifications.Role;
using ApplicationCore.UseCases.AuthenUser.Models;
using Mapster;
using Mediator;
using VELA.WebCoreBase.Core.Models;
using VELA.WebCoreBase.Libraries.Exceptions;

namespace ApplicationCore.UseCases.AuthenUser.Commands;
public sealed class Login : UserModel, VELA.WebCoreBase.Core.Mediators.ICommand<LoginDto>
{
    public sealed class Handler : ICommandHandler<Login, ResultModel<LoginDto>>
    {
        private readonly IAuthenUserRepository _authenRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IAuthenService _authenService;
        public Handler(
            IAuthenUserRepository authenRepository,
            IRoleRepository roleRepository,
            IAuthenService authenService)
        {
            _authenRepository = authenRepository;
            _roleRepository = roleRepository;
            _authenService = authenService;
        }
        public async ValueTask<ResultModel<LoginDto>> Handle(Login command, CancellationToken cancellationToken)
        {
            string enCode = _authenService.Encrypt(command.Password);

            AuthenUserByUsernamePassSpec loginSpec = new(command.Username, enCode);
            Entities.AuthenUser? user = await _authenRepository.FindOneAsync(loginSpec);
            if (user == null)
            {
                return ResultModel<LoginDto>.Create(new ValidationException(100036, $"Not found {command.Username} in system"));
            }

            string[] codeRoles = user.Roles.ToArray();

            RoleByArrayCodeSpec roleArraySpec = new(codeRoles);
            List<Entities.Role> roles = await _roleRepository.FindAsync(roleArraySpec);

            CreateUserDto userDto = user.Adapt<CreateUserDto>();

            // List of arrays to merge   
            if (roles is { Count: > 0 })
            {
                List<List<AuthenMenu>> allMenus = roles.Select(x => x.Menus).ToList();
                userDto.Menus = MergeArraysWithoutDuplicates(allMenus);
                userDto.Roles = roles.Where(d => user.Roles.Any(e => e == d.Code)).Select(e => e.Name).ToList();
            }

            LoginDto result = new()
            {
                expires_in = 43200,
                access_token = _authenService.GenerateToken(userDto),
                scope = "",
                id_token = "",
                token_type = "Bearer"
            };

            return ResultModel<LoginDto>.Create(result);
        }
        List<AuthenMenu> MergeArraysWithoutDuplicates(List<List<AuthenMenu>> arrays)
        {
            List<AuthenMenu> result = new();

            foreach (List<AuthenMenu> array in arrays)
            {
                foreach (AuthenMenu api in array)
                {
                    AuthenMenu? existingApi = result.FirstOrDefault(r => r.ApiCode == api.ApiCode);

                    if (existingApi == null)
                    {
                        // No matching ApiCode found, add the entire Api object
                        result.Add(api);
                    }
                    else
                    {
                        // ApiCode exists, merge Sites
                        foreach (MenuSite site in api.Sites)
                        {
                            MenuSite? existingSite = existingApi.Sites.FirstOrDefault(s => s.SiteCode == site.SiteCode);
                            if (existingSite == null)
                            {
                                // No matching SiteCode found, add the entire Site object
                                existingApi.Sites.Add(site);
                            }
                            else
                            {
                                // SiteCode exists, merge Features
                                foreach (Feature feature in site.Feature)
                                {
                                    Feature? existingFeature = existingSite.Feature.FirstOrDefault(f => f.FeatureCode == feature.FeatureCode);
                                    if (existingFeature == null)
                                    {
                                        // No matching FeatureCode found, add the Feature
                                        existingSite.Feature.Add(feature);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return result;
        }
    }
}
