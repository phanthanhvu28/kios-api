using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.DomainBusiness;
using ApplicationCore.DTOs.Area;
using ApplicationCore.UseCases.Area.Models;
using ApplicationCore.ValueObjects;
using Mapster;
using Mediator;
using VELA.WebCoreBase.Core.Models;
using VELA.WebCoreBase.Core.PipelineBehaviors;
using VELA.WebCoreBase.Libraries.Exceptions;

namespace ApplicationCore.UseCases.Area.Commands;
public class CreateArea : CreateAreaModel, VELA.WebCoreBase.Core.Mediators.ICommand<CreateAreaDto>
{
    public sealed class Handler : ICommandHandler<CreateArea, ResultModel<CreateAreaDto>>
    {
        private readonly IdentityUserObject? _identityUser;
        private readonly IAreaRepository _areaRepository;
        public Handler(IAppContextAccessor appContextAccessor, IAreaRepository areaRepository)
        {
            _identityUser = appContextAccessor.IdentityUser?.Adapt<IdentityUserObject>();
            _areaRepository = areaRepository;
        }
        public async ValueTask<ResultModel<CreateAreaDto>> Handle(CreateArea command, CancellationToken cancellationToken)
        {
            Entities.Areas @new = command.Adapt<Entities.Areas>();
            OneOf.OneOf<bool, CommonExceptionBase> process = @new.ProcessStep(new CreateNewProcess(_identityUser));
            if (process.IsT1)
            {
                return ResultModel<CreateAreaDto>.Create(process.AsT1);
            }
            _ = await _areaRepository.UpdateAsync(@new);

            return ResultModel<CreateAreaDto>.Create(@new.Adapt<CreateAreaDto>());
        }
    }
}
