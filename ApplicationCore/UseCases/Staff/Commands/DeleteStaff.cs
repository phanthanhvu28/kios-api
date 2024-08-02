using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.DomainBusiness;
using ApplicationCore.Specifications.Staff;
using ApplicationCore.ValueObjects;
using Mapster;
using Mediator;
using VELA.WebCoreBase.Core.Models;
using VELA.WebCoreBase.Core.PipelineBehaviors;
using VELA.WebCoreBase.Libraries.Exceptions;

namespace ApplicationCore.UseCases.Staff.Commands;
public class DeleteStaff : VELA.WebCoreBase.Core.Mediators.ICommand<string>
{
    public string Code { get; set; }
    public sealed class Handler : ICommandHandler<DeleteStaff, ResultModel<string>>
    {
        private readonly IdentityUserObject? _identityUser;
        private readonly IStaffRepository _staffRepository;
        public Handler(IAppContextAccessor appContextAccessor, IStaffRepository staffRepository)
        {
            _identityUser = appContextAccessor.IdentityUser?.Adapt<IdentityUserObject>();
            _staffRepository = staffRepository;
        }
        public async ValueTask<ResultModel<string>> Handle(DeleteStaff command, CancellationToken cancellationToken)
        {
            StaffByCodeSpec staffSpec = new(command.Code);
            Entities.Staffs? staff = await _staffRepository.FindOneAsync(staffSpec);
            if (staff == null)
            {
                return ResultModel<string>.Create(new NotFoundException(100036, $"Notfound staff:{command.Code}"));
            }

            OneOf.OneOf<bool, CommonExceptionBase> process = staff.ProcessStep(new DeleteProcess(_identityUser));
            if (process.IsT1)
            {
                return ResultModel<string>.Create(process.AsT1);
            }

            bool result = await _staffRepository.UpdateAsync(staff);
            if (!result)
            {
                return ResultModel<string>.Create(new NotFoundException(100036, $"Delete staff:{command.Code} error"));
            }

            return ResultModel<string>.Create($"Delete staff {command.Code} success");
        }
    }
}
