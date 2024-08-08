using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.DomainBusiness;
using ApplicationCore.DTOs.Table;
using ApplicationCore.UseCases.Table.Models;
using ApplicationCore.ValueObjects;
using Mapster;
using Mediator;
using VELA.WebCoreBase.Core.Models;
using VELA.WebCoreBase.Core.PipelineBehaviors;
using VELA.WebCoreBase.Libraries.Exceptions;

namespace ApplicationCore.UseCases.Table.Commands;
public class CreateTable : CreateTableModel, VELA.WebCoreBase.Core.Mediators.ICommand<CreateTableDto>
{
    public sealed class Handler : ICommandHandler<CreateTable, ResultModel<CreateTableDto>>
    {
        private readonly IdentityUserObject? _identityUser;
        private readonly ITableRepository _tableRepository;
        public Handler(IAppContextAccessor appContextAccessor, ITableRepository tableRepository)
        {
            _identityUser = appContextAccessor.IdentityUser?.Adapt<IdentityUserObject>();
            _tableRepository = tableRepository;
        }
        public async ValueTask<ResultModel<CreateTableDto>> Handle(CreateTable command, CancellationToken cancellationToken)
        {
            Entities.Tables @new = command.Adapt<Entities.Tables>();
            OneOf.OneOf<bool, CommonExceptionBase> process = @new.ProcessStep(new CreateNewProcess(_identityUser));
            if (process.IsT1)
            {
                return ResultModel<CreateTableDto>.Create(process.AsT1);
            }
            _ = await _tableRepository.UpdateAsync(@new);

            return ResultModel<CreateTableDto>.Create(@new.Adapt<CreateTableDto>());
        }
    }
}
