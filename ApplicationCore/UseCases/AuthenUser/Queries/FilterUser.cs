using ApplicationCore.Constants;
using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.DTOs.AuthenUser;
using ApplicationCore.Specifications.Company;
using ApplicationCore.ValueObjects;
using Mapster;
using Mediator;
using VELA.WebCoreBase.Core.Mediators;
using VELA.WebCoreBase.Core.Models;
using VELA.WebCoreBase.Core.PipelineBehaviors;

namespace ApplicationCore.UseCases.AuthenUser.Queries;
public class FilterUser : IItemQuery<FilterUserDto>
{
    public sealed class Handler : IQueryHandler<FilterUser, ResultModel<FilterUserDto>>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IdentityUserObject? _identityUser;

        public Handler(ICompanyRepository companyRepository,
           IAppContextAccessor appContextAccessor)
        {
            _companyRepository = companyRepository;
            _identityUser = appContextAccessor.IdentityUser?.Adapt<IdentityUserObject>();
        }
        public async ValueTask<ResultModel<FilterUserDto>> Handle(FilterUser query, CancellationToken cancellationToken)
        {
            FilterUserDto result = new();
            CompanyGetFilterSpec spec = new();
            List<Entities.Companies> company = await _companyRepository.FindAsync(spec);

            result.Store = company.Select(e => new ValueFilterObject { Value = new { e.Code, e.Name }, Label = e.Name }).ToList();
            result.Menus = AuthenSite.Site._menus;

            return ResultModel<FilterUserDto>.Create(result);
        }
    }
}
