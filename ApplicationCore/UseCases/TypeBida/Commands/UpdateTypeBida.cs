using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.DomainBusiness;
using ApplicationCore.DTOs.TypeBida;
using ApplicationCore.Specifications.TypeBida;
using ApplicationCore.UseCases.TypeBida.Models;
using ApplicationCore.ValueObjects;
using Mapster;
using Mediator;
using VELA.WebCoreBase.Core.Models;
using VELA.WebCoreBase.Core.PipelineBehaviors;
using VELA.WebCoreBase.Libraries.Exceptions;

namespace ApplicationCore.UseCases.TypeBida.Commands;
public class UpdateTypeBida : UpdateTypeBidaModel, VELA.WebCoreBase.Core.Mediators.ICommand<UpdateTypeBidaDto>
{
    public sealed class Handler : ICommandHandler<UpdateTypeBida, ResultModel<UpdateTypeBidaDto>>
    {
        private readonly IdentityUserObject? _identityUser;
        private readonly ITypeBidaRepository _typeBidaRepository;
        public Handler(IAppContextAccessor appContextAccessor, ITypeBidaRepository typeBidaRepository)
        {
            _identityUser = appContextAccessor.IdentityUser?.Adapt<IdentityUserObject>();
            _typeBidaRepository = typeBidaRepository;
        }
        public async ValueTask<ResultModel<UpdateTypeBidaDto>> Handle(UpdateTypeBida command, CancellationToken cancellationToken)
        {
            TypeBidaByCodeSpec typeBidaSpec = new(command.Code);
            Entities.TypeBida? typeBida = await _typeBidaRepository.FindOneAsync(typeBidaSpec);
            if (typeBida == null)
            {
                return ResultModel<UpdateTypeBidaDto>.Create(new NotFoundException(100036, $"Notfound type bida:{command.Code}"));
            }

            OneOf.OneOf<bool, CommonExceptionBase> process = typeBida.ProcessStep(new UpdateProcess(_identityUser));
            if (process.IsT1)
            {
                return ResultModel<UpdateTypeBidaDto>.Create(process.AsT1);
            }

            typeBida.Name = command.Name;
            typeBida.StoreCode = command.StoreCode;
            typeBida.StaffCode = command.StaffCode;

            bool result = await _typeBidaRepository.UpdateAsync(typeBida);
            if (!result)
            {
                return ResultModel<UpdateTypeBidaDto>.Create(new NotFoundException(100036, $"Update type bida:{command.Code} error"));
            }

            return ResultModel<UpdateTypeBidaDto>.Create(typeBida.Adapt<UpdateTypeBidaDto>());
        }
    }
}
