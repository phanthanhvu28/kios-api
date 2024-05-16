using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.DomainBusiness;
using ApplicationCore.DTOs.Company;
using ApplicationCore.UseCases.Company.Models;
using ApplicationCore.ValueObjects;
using Mapster;
using Mediator;
using VELA.WebCoreBase.Core.Models;
using VELA.WebCoreBase.Core.PipelineBehaviors;
using VELA.WebCoreBase.Libraries.Exceptions;

namespace ApplicationCore.UseCases.Company.Commands;
public sealed class CreateCompany : CreateCompanyModel, VELA.WebCoreBase.Core.Mediators.ICommand<CreateCompanyDto>
{
    public sealed class Handler : ICommandHandler<CreateCompany, ResultModel<CreateCompanyDto>>
    {
        private readonly IdentityUserObject? _identityUser;
        private readonly ICompanyRepository _companyRepository;
        public Handler(IAppContextAccessor appContextAccessor, ICompanyRepository companyRepository)
        {
            _identityUser = appContextAccessor.IdentityUser?.Adapt<IdentityUserObject>();
            _companyRepository = companyRepository;
        }
        public async ValueTask<ResultModel<CreateCompanyDto>> Handle(CreateCompany command, CancellationToken cancellationToken)
        {
            Entities.Companies @new = command.Adapt<Entities.Companies>();
            OneOf.OneOf<bool, CommonExceptionBase> process = @new.ProcessStep(new CreateNewProcess(_identityUser));
            if (process.IsT1)
            {
                return VELA.WebCoreBase.Core.Models.ResultModel<CreateCompanyDto>.Create(process.AsT1);
            }
            bool result = await _companyRepository.UpdateAsync(@new);

            return VELA.WebCoreBase.Core.Models.ResultModel<CreateCompanyDto>.Create(@new.Adapt<CreateCompanyDto>());
        }
    }
}
