using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.DomainBusiness;
using ApplicationCore.Specifications.Table;
using ApplicationCore.ValueObjects;
using Mapster;
using Mediator;
using VELA.WebCoreBase.Core.Models;
using VELA.WebCoreBase.Core.PipelineBehaviors;
using VELA.WebCoreBase.Libraries.Exceptions;

namespace ApplicationCore.UseCases.Table.Commands;
public class DeleteTable : VELA.WebCoreBase.Core.Mediators.ICommand<string>
{
    public string Code { get; set; }
    public sealed class Handler : ICommandHandler<DeleteTable, ResultModel<string>>
    {
        private readonly IdentityUserObject? _identityUser;
        private readonly ITableRepository _tableRepository;
        public Handler(IAppContextAccessor appContextAccessor, ITableRepository tableRepository)
        {
            _identityUser = appContextAccessor.IdentityUser?.Adapt<IdentityUserObject>();
            _tableRepository = tableRepository;
        }
        public async ValueTask<ResultModel<string>> Handle(DeleteTable command, CancellationToken cancellationToken)
        {
            TableByCodeSpec tableSpec = new(command.Code);
            Entities.Tables? table = await _tableRepository.FindOneAsync(tableSpec);
            if (table == null)
            {
                return ResultModel<string>.Create(new NotFoundException(100036, $"Notfound table:{command.Code}"));
            }

            OneOf.OneOf<bool, CommonExceptionBase> process = table.ProcessStep(new DeleteProcess(_identityUser));
            if (process.IsT1)
            {
                return ResultModel<string>.Create(process.AsT1);
            }

            bool result = await _tableRepository.UpdateAsync(table);
            if (!result)
            {
                return ResultModel<string>.Create(new NotFoundException(100036, $"Delete table:{command.Code} error"));
            }

            return ResultModel<string>.Create($"Delete table {command.Code} success");
        }
    }
}
