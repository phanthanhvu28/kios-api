using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.DomainBusiness;
using ApplicationCore.DTOs.Company;
using ApplicationCore.Specifications.Company;
using ApplicationCore.UseCases.Company.Models;
using ApplicationCore.ValueObjects;
using Mapster;
using Mediator;
using VELA.WebCoreBase.Core.Models;
using VELA.WebCoreBase.Core.PipelineBehaviors;
using VELA.WebCoreBase.Libraries.Exceptions;

namespace ApplicationCore.UseCases.Company.Commands;
public sealed class UpdateCompany : UpdateCompanyModel, VELA.WebCoreBase.Core.Mediators.ICommand<UpdateCompanyDto>
{
    public sealed class Handler : ICommandHandler<UpdateCompany, ResultModel<UpdateCompanyDto>>
    {
        private readonly IdentityUserObject? _identityUser;
        private readonly ICompanyRepository _companyRepository;
        public Handler(IAppContextAccessor appContextAccessor, ICompanyRepository companyRepository)
        {
            _identityUser = appContextAccessor.IdentityUser?.Adapt<IdentityUserObject>();
            _companyRepository = companyRepository;
        }
        public async ValueTask<ResultModel<UpdateCompanyDto>> Handle(UpdateCompany command, CancellationToken cancellationToken)
        {
            CompanyByCodeSpec companySpec = new(command.Code);
            Entities.Companies? company = await _companyRepository.FindOneAsync(companySpec);
            if (company == null)
            {
                return ResultModel<UpdateCompanyDto>.Create(new NotFoundException(100001, $"company:{command.Code}"));
            }

            OneOf.OneOf<bool, CommonExceptionBase> process = company.ProcessStep(new UpdateProcess(_identityUser));
            if (process.IsT1)
            {
                return ResultModel<UpdateCompanyDto>.Create(process.AsT1);
            }

            company.Name = command.Name;
            company.Address = command.Address;
            company.Email = command.Email;
            company.Phone = command.Phone;

            bool result = await _companyRepository.UpdateAsync(company);
            if (!result)
            {
                return ResultModel<UpdateCompanyDto>.Create(new NotFoundException(100001, $"Update company:{command.Code} error"));
            }

            return ResultModel<UpdateCompanyDto>.Create(company.Adapt<UpdateCompanyDto>());
        }
    }
}
