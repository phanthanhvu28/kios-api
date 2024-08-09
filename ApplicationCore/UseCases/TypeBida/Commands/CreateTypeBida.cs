using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.DomainBusiness;
using ApplicationCore.DTOs.TypeBida;
using ApplicationCore.UseCases.TypeBida.Models;
using ApplicationCore.ValueObjects;
using Mapster;
using Mediator;
using VELA.WebCoreBase.Core.Models;
using VELA.WebCoreBase.Core.PipelineBehaviors;
using VELA.WebCoreBase.Libraries.Exceptions;

namespace ApplicationCore.UseCases.TypeBida.Commands;
public class CreateTypeBida : CreateTypeBidaModel, VELA.WebCoreBase.Core.Mediators.ICommand<CreateTypeBidaDto>
{
    public sealed class Handler : ICommandHandler<CreateTypeBida, ResultModel<CreateTypeBidaDto>>
    {
        private readonly IdentityUserObject? _identityUser;
        private readonly ITypeBidaRepository _typeBidaRepository;
        public Handler(IAppContextAccessor appContextAccessor, ITypeBidaRepository typeBidaRepository)
        {
            _identityUser = appContextAccessor.IdentityUser?.Adapt<IdentityUserObject>();
            _typeBidaRepository = typeBidaRepository;
        }
        public async ValueTask<ResultModel<CreateTypeBidaDto>> Handle(CreateTypeBida command, CancellationToken cancellationToken)
        {
            Entities.TypeBida @new = command.Adapt<Entities.TypeBida>();
            OneOf.OneOf<bool, CommonExceptionBase> process = @new.ProcessStep(new CreateNewProcess(_identityUser));
            if (process.IsT1)
            {
                return ResultModel<CreateTypeBidaDto>.Create(process.AsT1);
            }
            _ = await _typeBidaRepository.UpdateAsync(@new);

            return ResultModel<CreateTypeBidaDto>.Create(@new.Adapt<CreateTypeBidaDto>());
        }
    }
}
