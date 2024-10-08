﻿using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.DTOs.AuthenUser;
using ApplicationCore.Specifications.AuthenUser;
using ApplicationCore.Specifications.Role;
using ApplicationCore.Specifications.Store;
using ApplicationCore.ValueObjects;
using Mapster;
using Mediator;
using VELA.WebCoreBase.Core.Mediators;
using VELA.WebCoreBase.Core.Models;
using VELA.WebCoreBase.Core.PipelineBehaviors;

namespace ApplicationCore.UseCases.AuthenUser.Queries;
public class GetUser : PagingModel, IListQuery<UserBaseDto>
{
    public sealed class Handler : IQueryHandler<GetUser, ListResultModel<UserBaseDto>>
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IAuthenUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IdentityUserObject? _identityUser;

        public Handler(
            IStoreRepository storeRepository,
            IAuthenUserRepository userRepository,
            IRoleRepository roleRepository,
            IAppContextAccessor appContextAccessor)
        {
            _storeRepository = storeRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _identityUser = appContextAccessor.IdentityUser?.Adapt<IdentityUserObject>();
        }
        public async ValueTask<ListResultModel<UserBaseDto>> Handle(GetUser query, CancellationToken cancellationToken)
        {
            AuthenUserGetAllSpec spec = new(query.Filters, query.Sorts, query.Page, query.PageSize);
            List<Entities.AuthenUser> entity = await _userRepository.FindAsync(spec);
            List<UserBaseDto> result = entity.Adapt<List<UserBaseDto>>();

            StoreByArrayCodeSpec storeArraySpec = new(entity.Select(e => e.StoreCode).ToArray());
            List<Entities.Stores> stores = await _storeRepository.FindAsync(storeArraySpec);

            string[] codeRoles = entity.SelectMany(e => e.Roles).ToArray();

            RoleByArrayCodeSpec roleArraySpec = new(codeRoles);
            List<Entities.Role> roles = await _roleRepository.FindAsync(roleArraySpec);

            Dictionary<string, string> storeDictionary = stores.ToDictionary(c => c.Code, c => c.Name);

            result.ForEach(storedto =>
            {
                if (storeDictionary.TryGetValue(storedto.StoreCode, out string? storeName))
                {
                    storedto.StoreName = storeName;
                }
                else
                {
                    storedto.StoreName = "";
                }
            });

            result.ForEach(roledto =>
            {
                roledto.ModelRoles = roles.Where(d => roledto.Roles.Any(a => a == d.Code)).Select(e => new RoleDto { Code = e.Code, Name = e.Name }).ToList();
            });

            long entityCount = await _userRepository.CountAsync(spec);
            return ListResultModel<UserBaseDto>.Create(result, entityCount, query.Page, query.PageSize);
        }
    }
}
