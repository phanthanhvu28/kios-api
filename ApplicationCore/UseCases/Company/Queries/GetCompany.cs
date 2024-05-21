using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.DTOs.Company;
using ApplicationCore.Specifications.Company;
using ApplicationCore.ValueObjects;
using Mapster;
using Mediator;
using VELA.WebCoreBase.Core.Mediators;
using VELA.WebCoreBase.Core.Models;
using VELA.WebCoreBase.Core.PipelineBehaviors;

namespace ApplicationCore.UseCases.Company.Queries;
public class GetCompany : PagingModel, IListQuery<CompanyBaseDto>
{
    public sealed class Handler : IQueryHandler<GetCompany, ListResultModel<CompanyBaseDto>>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IdentityUserObject? _identityUser;

        public Handler(ICompanyRepository companyRepository,
           IAppContextAccessor appContextAccessor)
        {
            _companyRepository = companyRepository;
            _identityUser = appContextAccessor.IdentityUser?.Adapt<IdentityUserObject>();
        }
        public async ValueTask<ListResultModel<CompanyBaseDto>> Handle(GetCompany query, CancellationToken cancellationToken)
        {
            CompanyByFilterSpec spec = new(query.Filters, query.Sorts, query.Page, query.PageSize);
            List<Entities.Companies> company = await _companyRepository.FindAsync(spec);
            long companyCount = await _companyRepository.CountAsync(spec);
            return ListResultModel<CompanyBaseDto>.Create(
                 company.Adapt<List<CompanyBaseDto>>(), companyCount, query.Page, query.PageSize);
        }
    }
}
