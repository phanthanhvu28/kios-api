using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.DomainBusiness;
using ApplicationCore.DTOs.Area;
using ApplicationCore.Specifications.Area;
using ApplicationCore.UseCases.Area.Models;
using ApplicationCore.ValueObjects;
using Mapster;
using Mediator;
using VELA.WebCoreBase.Core.Models;
using VELA.WebCoreBase.Core.PipelineBehaviors;
using VELA.WebCoreBase.Libraries.Exceptions;

namespace ApplicationCore.UseCases.Area.Commands;
public class UpdateArea : UpdateAreaModel, VELA.WebCoreBase.Core.Mediators.ICommand<UpdateAreaDto>
{
    public sealed class Handler : ICommandHandler<UpdateArea, ResultModel<UpdateAreaDto>>
    {
        private readonly IdentityUserObject? _identityUser;
        private readonly IAreaRepository _areaRepository;
        public Handler(IAppContextAccessor appContextAccessor, IAreaRepository staffRepository)
        {
            _identityUser = appContextAccessor.IdentityUser?.Adapt<IdentityUserObject>();
            _areaRepository = staffRepository;
        }
        public async ValueTask<ResultModel<UpdateAreaDto>> Handle(UpdateArea command, CancellationToken cancellationToken)
        {
            AreaByCodeSpec staffSpec = new(command.Code);
            Entities.Areas? area = await _areaRepository.FindOneAsync(staffSpec);
            if (area == null)
            {
                return ResultModel<UpdateAreaDto>.Create(new NotFoundException(100036, $"Notfound area:{command.Code}"));
            }

            OneOf.OneOf<bool, CommonExceptionBase> process = area.ProcessStep(new UpdateProcess(_identityUser));
            if (process.IsT1)
            {
                return ResultModel<UpdateAreaDto>.Create(process.AsT1);
            }

            area.Name = command.Name;
            area.Address = command.Address;
            area.Email = command.Email;
            area.Phone = command.Phone;
            area.StoreCode = command.StoreCode;
            area.StaffCode = command.StaffCode;

            bool result = await _areaRepository.UpdateAsync(area);
            if (!result)
            {
                return ResultModel<UpdateAreaDto>.Create(new NotFoundException(100036, $"Update area:{command.Code} error"));
            }

            return ResultModel<UpdateAreaDto>.Create(area.Adapt<UpdateAreaDto>());
        }
    }
}
