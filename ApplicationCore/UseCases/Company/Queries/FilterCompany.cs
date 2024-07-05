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
public class FilterCompany : IItemQuery<FilterCompanyDto>
{
    public sealed class Handler : IQueryHandler<FilterCompany, ResultModel<FilterCompanyDto>>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IdentityUserObject? _identityUser;

        public Handler(ICompanyRepository companyRepository,
           IAppContextAccessor appContextAccessor)
        {
            _companyRepository = companyRepository;
            _identityUser = appContextAccessor.IdentityUser?.Adapt<IdentityUserObject>();
        }
        public async ValueTask<ResultModel<FilterCompanyDto>> Handle(FilterCompany query, CancellationToken cancellationToken)
        {
            FilterCompanyDto result = new();
            CompanyGetFilterSpec spec = new();
            List<Entities.Companies> company = await _companyRepository.FindAsync(spec);

            result.Company = company.Select(e => new ValueFilterObject { Value = new { e.Code, e.Name }, Label = e.Name }).ToList();

            return ResultModel<FilterCompanyDto>.Create(result);
        }
    }
}
