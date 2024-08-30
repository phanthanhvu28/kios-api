using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.DTOs.Product;
using ApplicationCore.Specifications.Product;
using ApplicationCore.Specifications.Store;
using ApplicationCore.Specifications.TypeBida;
using ApplicationCore.Specifications.TypeSale;
using ApplicationCore.ValueObjects;
using Mapster;
using Mediator;
using VELA.WebCoreBase.Core.Mediators;
using VELA.WebCoreBase.Core.Models;
using VELA.WebCoreBase.Core.PipelineBehaviors;

namespace ApplicationCore.UseCases.Product.Queries;
public class GetProduct : PagingModel, IListQuery<ProductBaseDto>
{
    public sealed class Handler : IQueryHandler<GetProduct, ListResultModel<ProductBaseDto>>
    {
        private readonly IStaffRepository _staffRepository;
        private readonly IProductRepository _productRepository;
        private readonly IStoreRepository _storeRepository;
        private readonly ITypeSaleRepository _typeSaleRepository;
        private readonly ITypeBidaRepository _typeBidaRepository;
        private readonly IdentityUserObject? _identityUser;

        public Handler(
            IStaffRepository staffRepository,
            IStoreRepository storeRepository,
            IProductRepository productRepository,
            ITypeSaleRepository typeSaleRepository,
            ITypeBidaRepository typeBidaRepository,
            IAppContextAccessor appContextAccessor)
        {
            _staffRepository = staffRepository;
            _storeRepository = storeRepository;
            _productRepository = productRepository;
            _typeSaleRepository = typeSaleRepository;
            _typeBidaRepository = typeBidaRepository;
            _identityUser = appContextAccessor.IdentityUser?.Adapt<IdentityUserObject>();
        }
        public async ValueTask<ListResultModel<ProductBaseDto>> Handle(GetProduct query, CancellationToken cancellationToken)
        {
            ProductByFilterSpec spec = new(query.Filters, query.Sorts, query.Page, query.PageSize);
            List<Entities.Products> entity = await _productRepository.FindAsync(spec);
            List<ProductBaseDto> result = entity.Adapt<List<ProductBaseDto>>();

            StoreByArrayCodeSpec storeSpec = new(entity.Select(e => e.StoreCode).ToArray());
            List<Entities.Stores> stores = await _storeRepository.FindAsync(storeSpec);

            Dictionary<string, string> companyDictionary = stores.ToDictionary(c => c.Code, c => c.Name);

            result.ForEach(storeDto =>
            {
                if (companyDictionary.TryGetValue(storeDto.StoreCode, out string? companyName))
                {
                    storeDto.StoreName = companyName;
                }
                else
                {
                    storeDto.StoreName = "";
                }
            });
            TypeBidaByArrayCodeSpec typeBidaSpec = new(entity.Select(e => e.TypeBidaCode).ToArray());
            List<Entities.TypeBida> typeBida = await _typeBidaRepository.FindAsync(typeBidaSpec);

            Dictionary<string, string> typeBidaDictionary = typeBida.ToDictionary(c => c.Code, c => c.Name);

            result.ForEach(typeBidaDto =>
            {
                if (typeBidaDictionary.TryGetValue(typeBidaDto.TypeBidaCode, out string? typeBidaName))
                {
                    typeBidaDto.TypeBidaName = typeBidaName;
                }
                else
                {
                    typeBidaDto.TypeBidaName = "";
                }
            });

            TypeSaleByArrayCodeSpec typeSaleSpec = new(entity.Select(e => e.TypeSaleCode).ToArray());
            List<Entities.TypeSales> typeSale = await _typeSaleRepository.FindAsync(typeSaleSpec);

            Dictionary<string, string> typeSaleDictionary = typeSale.ToDictionary(c => c.Code, c => c.Name);

            result.ForEach(typeSaleDto =>
            {
                if (typeSaleDictionary.TryGetValue(typeSaleDto.TypeSaleCode, out string? typeSaleName))
                {
                    typeSaleDto.TypeSaleName = typeSaleName;
                }
                else
                {
                    typeSaleDto.TypeSaleName = "";
                }
            });

            long entityCount = await _productRepository.CountAsync(spec);
            return ListResultModel<ProductBaseDto>.Create(result, entityCount, query.Page, query.PageSize);
        }
    }
}
