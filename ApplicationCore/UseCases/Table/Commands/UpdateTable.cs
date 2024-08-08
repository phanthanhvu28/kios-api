using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.DomainBusiness;
using ApplicationCore.DTOs.Table;
using ApplicationCore.Specifications.Table;
using ApplicationCore.UseCases.Table.Models;
using ApplicationCore.ValueObjects;
using Mapster;
using Mediator;
using VELA.WebCoreBase.Core.Models;
using VELA.WebCoreBase.Core.PipelineBehaviors;
using VELA.WebCoreBase.Libraries.Exceptions;

namespace ApplicationCore.UseCases.Table.Commands;
public class UpdateTable : UpdateTableModel, VELA.WebCoreBase.Core.Mediators.ICommand<UpdateTableDto>
{
    public sealed class Handler : ICommandHandler<UpdateTable, ResultModel<UpdateTableDto>>
    {
        private readonly IdentityUserObject? _identityUser;
        private readonly ITableRepository _tableRepository;
        public Handler(IAppContextAccessor appContextAccessor, ITableRepository tableRepository)
        {
            _identityUser = appContextAccessor.IdentityUser?.Adapt<IdentityUserObject>();
            _tableRepository = tableRepository;
        }
        public async ValueTask<ResultModel<UpdateTableDto>> Handle(UpdateTable command, CancellationToken cancellationToken)
        {
            TableByCodeSpec tableSpec = new(command.Code);
            Entities.Tables? table = await _tableRepository.FindOneAsync(tableSpec);
            if (table == null)
            {
                return ResultModel<UpdateTableDto>.Create(new NotFoundException(100036, $"Notfound table:{command.Code}"));
            }

            OneOf.OneOf<bool, CommonExceptionBase> process = table.ProcessStep(new UpdateProcess(_identityUser));
            if (process.IsT1)
            {
                return ResultModel<UpdateTableDto>.Create(process.AsT1);
            }

            table.Name = command.Name;
            table.Address = command.Address;
            table.Email = command.Email;
            table.Phone = command.Phone;
            table.StoreCode = command.StoreCode;
            table.StaffCode = command.StaffCode;
            table.AreaCode = command.AreaCode;
            table.TypeBidaCode = command.TypeBidaCode;
            table.TypeSaleCode = command.TypeSaleCode;

            bool result = await _tableRepository.UpdateAsync(table);
            if (!result)
            {
                return ResultModel<UpdateTableDto>.Create(new NotFoundException(100036, $"Update table:{command.Code} error"));
            }

            return ResultModel<UpdateTableDto>.Create(table.Adapt<UpdateTableDto>());
        }
    }
}
