using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.DomainBusiness;
using ApplicationCore.Specifications.Area;
using ApplicationCore.ValueObjects;
using Mapster;
using Mediator;
using VELA.WebCoreBase.Core.Models;
using VELA.WebCoreBase.Core.PipelineBehaviors;
using VELA.WebCoreBase.Libraries.Exceptions;

namespace ApplicationCore.UseCases.Area.Commands;
public class DeleteArea : VELA.WebCoreBase.Core.Mediators.ICommand<string>
{
    public string Code { get; set; }
    public sealed class Handler : ICommandHandler<DeleteArea, ResultModel<string>>
    {
        private readonly IdentityUserObject? _identityUser;
        private readonly IAreaRepository _areaRepository;
        public Handler(IAppContextAccessor appContextAccessor, IAreaRepository areaRepository)
        {
            _identityUser = appContextAccessor.IdentityUser?.Adapt<IdentityUserObject>();
            _areaRepository = areaRepository;
        }
        public async ValueTask<ResultModel<string>> Handle(DeleteArea command, CancellationToken cancellationToken)
        {
            AreaByCodeSpec areaSpec = new(command.Code);
            Entities.Areas? area = await _areaRepository.FindOneAsync(areaSpec);
            if (area == null)
            {
                return ResultModel<string>.Create(new NotFoundException(100036, $"Notfound area:{command.Code}"));
            }

            OneOf.OneOf<bool, CommonExceptionBase> process = area.ProcessStep(new DeleteProcess(_identityUser));
            if (process.IsT1)
            {
                return ResultModel<string>.Create(process.AsT1);
            }

            bool result = await _areaRepository.UpdateAsync(area);
            if (!result)
            {
                return ResultModel<string>.Create(new NotFoundException(100036, $"Delete area:{command.Code} error"));
            }

            return ResultModel<string>.Create($"Delete area {command.Code} success");
        }
    }
}
