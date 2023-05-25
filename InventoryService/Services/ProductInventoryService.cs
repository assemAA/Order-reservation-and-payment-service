using Grpc.Core;
namespace InventoryService.Services;
public class ProductInventoryService : InventoryService.InventoryServiceBase
{
    private readonly ILogger<ProductInventoryService> _logger;

    private readonly List<Product> _products = new List<Product>()
    {
        new Product(){ProductId=1,Quantity=10},
        new Product(){ProductId=2,Quantity=8},
        new Product(){ProductId=3,Quantity=2},
        new Product(){ProductId=4,Quantity=30},
        new Product(){ProductId=5,Quantity=7},
    };

    public ProductInventoryService(ILogger<ProductInventoryService> _logger)
    {
        this._logger = _logger;
    }
    public override Task<response> CheckQuantity(Product request, ServerCallContext context)
    {
        _logger.LogInformation($"Request For ProductId {request.ProductId}");
        var product = _products.FirstOrDefault(P => P.ProductId == request.ProductId);
        if (product == null) 
            return Task.FromResult(new response() { Available = false,Message = "Product Not Found"});
        if (product.Quantity < request.Quantity)
            return Task.FromResult(new response() { Available = false, Message = "Insufficient Quantity" });
        return Task.FromResult(new response() { Available = true, Message = "Ok" });
    }
}