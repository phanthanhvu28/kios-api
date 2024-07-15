using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.DTOs.AuthenUser;
using ApplicationCore.Specifications.AuthenUser;
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
        private readonly IdentityUserObject? _identityUser;

        public Handler(
            IStoreRepository storeRepository,
            IAuthenUserRepository userRepository,
           IAppContextAccessor appContextAccessor)
        {
            _storeRepository = storeRepository;
            _userRepository = userRepository;
            _identityUser = appContextAccessor.IdentityUser?.Adapt<IdentityUserObject>();
        }
        public async ValueTask<ListResultModel<UserBaseDto>> Handle(GetUser query, CancellationToken cancellationToken)
        {
            AuthenUserGetAllSpec spec = new(query.Filters, query.Sorts, query.Page, query.PageSize);
            List<Entities.AuthenUser> entity = await _userRepository.FindAsync(spec);
            List<UserBaseDto> result = entity.Adapt<List<UserBaseDto>>();

            StoreByArrayCodeSpec storeArraySpec = new(entity.Select(e => e.StoreCode).ToArray());
            List<Entities.Stores> stores = await _storeRepository.FindAsync(storeArraySpec);

            Dictionary<string, string> storeDictionary = stores.ToDictionary(c => c.Code, c => c.Name);

            result.ForEach(storedto =>
            {
                if (storeDictionary.TryGetValue(storedto.StoreCode, out string? companyname))
                {
                    storedto.StoreName = companyname;
                }
                else
                {
                    storedto.StoreName = "";
                }
            });

            long entityCount = await _userRepository.CountAsync(spec);
            return ListResultModel<UserBaseDto>.Create(result, entityCount, query.Page, query.PageSize);
        }
    }
}
