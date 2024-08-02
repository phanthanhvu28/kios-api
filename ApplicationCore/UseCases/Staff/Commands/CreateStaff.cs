using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.DomainBusiness;
using ApplicationCore.DTOs.Staff;
using ApplicationCore.UseCases.Staff.Models;
using ApplicationCore.ValueObjects;
using Mapster;
using Mediator;
using VELA.WebCoreBase.Core.Models;
using VELA.WebCoreBase.Core.PipelineBehaviors;
using VELA.WebCoreBase.Libraries.Exceptions;

namespace ApplicationCore.UseCases.Staff.Commands;
public class CreateStaff : CreateStaffModel, VELA.WebCoreBase.Core.Mediators.ICommand<CreateStaffDto>
{
    public sealed class Handler : ICommandHandler<CreateStaff, ResultModel<CreateStaffDto>>
    {
        private readonly IdentityUserObject? _identityUser;
        private readonly IStaffRepository _staffRepository;
        public Handler(IAppContextAccessor appContextAccessor, IStaffRepository staffRepository)
        {
            _identityUser = appContextAccessor.IdentityUser?.Adapt<IdentityUserObject>();
            _staffRepository = staffRepository;
        }
        public async ValueTask<ResultModel<CreateStaffDto>> Handle(CreateStaff command, CancellationToken cancellationToken)
        {
            Entities.Staffs @new = command.Adapt<Entities.Staffs>();
            OneOf.OneOf<bool, CommonExceptionBase> process = @new.ProcessStep(new CreateNewProcess(_identityUser));
            if (process.IsT1)
            {
                return ResultModel<CreateStaffDto>.Create(process.AsT1);
            }
            _ = await _staffRepository.UpdateAsync(@new);

            return ResultModel<CreateStaffDto>.Create(@new.Adapt<CreateStaffDto>());
        }
    }
}
