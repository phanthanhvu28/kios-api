using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.DomainBusiness;
using ApplicationCore.Specifications.TypeBida;
using ApplicationCore.ValueObjects;
using Mapster;
using Mediator;
using VELA.WebCoreBase.Core.Models;
using VELA.WebCoreBase.Core.PipelineBehaviors;
using VELA.WebCoreBase.Libraries.Exceptions;

namespace ApplicationCore.UseCases.TypeBida.Commands;
public class DeleteTypeBida : VELA.WebCoreBase.Core.Mediators.ICommand<string>
{
    public string Code { get; set; }
    public sealed class Handler : ICommandHandler<DeleteTypeBida, ResultModel<string>>
    {
        private readonly IdentityUserObject? _identityUser;
        private readonly ITypeBidaRepository _typeBidaRepository;
        public Handler(IAppContextAccessor appContextAccessor, ITypeBidaRepository typeBidaRepository)
        {
            _identityUser = appContextAccessor.IdentityUser?.Adapt<IdentityUserObject>();
            _typeBidaRepository = typeBidaRepository;
        }
        public async ValueTask<ResultModel<string>> Handle(DeleteTypeBida command, CancellationToken cancellationToken)
        {
            TypeBidaByCodeSpec typeBidaSpec = new(command.Code);
            Entities.TypeBida? typeBida = await _typeBidaRepository.FindOneAsync(typeBidaSpec);
            if (typeBida == null)
            {
                return ResultModel<string>.Create(new NotFoundException(100036, $"Notfound type bida:{command.Code}"));
            }

            OneOf.OneOf<bool, CommonExceptionBase> process = typeBida.ProcessStep(new DeleteProcess(_identityUser));
            if (process.IsT1)
            {
                return ResultModel<string>.Create(process.AsT1);
            }

            bool result = await _typeBidaRepository.UpdateAsync(typeBida);
            if (!result)
            {
                return ResultModel<string>.Create(new NotFoundException(100036, $"Delete type bida:{command.Code} error"));
            }

            return ResultModel<string>.Create($"Delete type bida {command.Code} success");
        }
    }
}
