using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.DomainBusiness;
using ApplicationCore.DTOs.Staff;
using ApplicationCore.Specifications.Staff;
using ApplicationCore.UseCases.Staff.Models;
using ApplicationCore.ValueObjects;
using Mapster;
using Mediator;
using VELA.WebCoreBase.Core.Models;
using VELA.WebCoreBase.Core.PipelineBehaviors;
using VELA.WebCoreBase.Libraries.Exceptions;

namespace ApplicationCore.UseCases.Staff.Commands;
public class UpdateStaff : UpdateStaffModel, VELA.WebCoreBase.Core.Mediators.ICommand<UpdateStaffDto>
{
    public sealed class Handler : ICommandHandler<UpdateStaff, ResultModel<UpdateStaffDto>>
    {
        private readonly IdentityUserObject? _identityUser;
        private readonly IStaffRepository _staffRepository;
        public Handler(IAppContextAccessor appContextAccessor, IStaffRepository staffRepository)
        {
            _identityUser = appContextAccessor.IdentityUser?.Adapt<IdentityUserObject>();
            _staffRepository = staffRepository;
        }
        public async ValueTask<ResultModel<UpdateStaffDto>> Handle(UpdateStaff command, CancellationToken cancellationToken)
        {
            StaffByCodeSpec staffSpec = new(command.Code);
            Entities.Staffs? staff = await _staffRepository.FindOneAsync(staffSpec);
            if (staff == null)
            {
                return ResultModel<UpdateStaffDto>.Create(new NotFoundException(100036, $"Notfound staff:{command.Code}"));
            }

            OneOf.OneOf<bool, CommonExceptionBase> process = staff.ProcessStep(new UpdateProcess(_identityUser));
            if (process.IsT1)
            {
                return ResultModel<UpdateStaffDto>.Create(process.AsT1);
            }

            staff.FullName = command.FullName;
            staff.Address = command.Address;
            staff.Email = command.Email;
            staff.Phone = command.Phone;
            staff.StoreCode = command.StoreCode;

            bool result = await _staffRepository.UpdateAsync(staff);
            if (!result)
            {
                return ResultModel<UpdateStaffDto>.Create(new NotFoundException(100036, $"Update staff:{command.Code} error"));
            }

            return ResultModel<UpdateStaffDto>.Create(staff.Adapt<UpdateStaffDto>());
        }
    }
}
