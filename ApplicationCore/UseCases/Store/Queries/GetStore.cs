using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.DTOs.Store;
using ApplicationCore.Specifications.Company;
using ApplicationCore.Specifications.Store;
using ApplicationCore.ValueObjects;
using Mapster;
using Mediator;
using VELA.WebCoreBase.Core.Mediators;
using VELA.WebCoreBase.Core.Models;
using VELA.WebCoreBase.Core.PipelineBehaviors;

namespace ApplicationCore.UseCases.Store.Queries;
public class GetStore : PagingModel, IListQuery<StoreBaseDto>
{
    public sealed class Handler : IQueryHandler<GetStore, ListResultModel<StoreBaseDto>>
    {
        private readonly IStoreRepository _storeRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IdentityUserObject? _identityUser;

        public Handler(
            IStoreRepository storeRepository,
            ICompanyRepository companyRepository,
           IAppContextAccessor appContextAccessor)
        {
            _storeRepository = storeRepository;
            _companyRepository = companyRepository;
            _identityUser = appContextAccessor.IdentityUser?.Adapt<IdentityUserObject>();
        }
        public async ValueTask<ListResultModel<StoreBaseDto>> Handle(GetStore query, CancellationToken cancellationToken)
        {
            StoreByFilterSpec spec = new(query.Filters, query.Sorts, query.Page, query.PageSize);
            List<Entities.Stores> entity = await _storeRepository.FindAsync(spec);
            List<StoreBaseDto> result = entity.Adapt<List<StoreBaseDto>>();

            CompanyByArrayCodeSpec companySpec = new(entity.Select(e => e.CompanyCode).ToArray());
            List<Entities.Companies> companies = await _companyRepository.FindAsync(companySpec);

            Dictionary<string, string> companyDictionary = companies.ToDictionary(c => c.Code, c => c.Name);

            result.ForEach(companyDto =>
            {
                if (companyDictionary.TryGetValue(companyDto.CompanyCode, out string? companyName))
                {
                    companyDto.CompanyName = companyName;
                }
                else
                {
                    companyDto.CompanyName = "";
                }
            });

            long entityCount = await _storeRepository.CountAsync(spec);
            return ListResultModel<StoreBaseDto>.Create(result, entityCount, query.Page, query.PageSize);
        }
    }
}
