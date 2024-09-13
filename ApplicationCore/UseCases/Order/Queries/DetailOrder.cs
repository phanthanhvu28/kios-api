using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.DTOs.Order;
using ApplicationCore.DTOs.OrderDetail;
using ApplicationCore.Specifications.Order;
using ApplicationCore.Specifications.Product;
using ApplicationCore.ValueObjects;
using Mapster;
using Mediator;
using VELA.WebCoreBase.Core.Models;
using VELA.WebCoreBase.Core.PipelineBehaviors;
using VELA.WebCoreBase.Libraries.Exceptions;

namespace ApplicationCore.UseCases.Order.Queries;
public class DetailOrder : VELA.WebCoreBase.Core.Mediators.IItemQuery<OrderDetailDto>
{
    public string OrderCode { get; set; }
    public sealed class Handler : IQueryHandler<DetailOrder, ResultModel<OrderDetailDto>>
    {

        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IProductRepository _productRepository;
        private readonly IdentityUserObject? _identityUser;

        public Handler(
            IOrderRepository orderRepository,
            IOrderDetailRepository orderDetailRepository,
            IProductRepository productRepository,
            IAppContextAccessor appContextAccessor)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _productRepository = productRepository;
            _identityUser = appContextAccessor.IdentityUser?.Adapt<IdentityUserObject>();
        }
        public async ValueTask<ResultModel<OrderDetailDto>> Handle(DetailOrder query, CancellationToken cancellationToken)
        {
            OrderDetailDto result = new();

            OrderByCodeSpec orderSpec = new(query.OrderCode);
            Entities.Orders? order = await _orderRepository.FindOneAsync(orderSpec);
            if (order is null)
            {
                return ResultModel<OrderDetailDto>.Create(new ValidationException(100036, $"Not Found order {query.OrderCode}"));
            }

            OrderDetailByOrderSpec orderDetailSpec = new(query.OrderCode);
            List<Entities.OrderDetails> orderDetailNew = await _orderDetailRepository.FindAsync(orderDetailSpec);

            result.Items = orderDetailNew.Adapt<List<OrderDetailBaseDto>>();
            result.Order = order.Adapt<OrderBaseDto>();

            List<OrderDetailBaseDto> items = orderDetailNew.Adapt<List<OrderDetailBaseDto>>();

            ProductByArrayCodeSpec productSpec = new(items.Select(e => e.ProductCode).ToArray());
            List<Entities.Products> products = await _productRepository.FindAsync(productSpec);

            Dictionary<string, string> productDictionary = products.ToDictionary(c => c.Code, c => c.Name);

            UpdateProductNames(result.Items, productDictionary);

            return ResultModel<OrderDetailDto>.Create(result);
        }
        private void UpdateProductNames(List<OrderDetailBaseDto> orderDetails, Dictionary<string, string> productDictionary)
        {
            orderDetails.ForEach(orderDetailDto =>
            {
                orderDetailDto.ProductName = productDictionary.TryGetValue(orderDetailDto.ProductCode, out string? productName)
                    ? productName
                    : string.Empty;
            });
        }
    }
}
