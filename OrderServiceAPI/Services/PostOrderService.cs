using Grpc.Core;
using Grpc.Net.Client;
using InventoryService;
using Microsoft.AspNetCore.Http.HttpResults;
using OrderServiceAPI;
using OrderServiceAPI.APIProto;
using PaymentService;

namespace OrderServiceAPI.Services;

public class PostOrderService:OrderService.OrderServiceBase
{
    private readonly ILogger<PostOrderService> _logger;

    public PostOrderService(ILogger<PostOrderService> _logger)
    {
        this._logger = _logger;
    }
    public override async Task<PostResponse> PostOrder(Order order, ServerCallContext context)
    {
        _logger.LogInformation($"Request For Customer {order.CustomerId}");
        var inventoryChannel = GrpcChannel.ForAddress("https://localhost:7078");
        var inventoryClient = new InventoryService.InventoryService.InventoryServiceClient(inventoryChannel);
        var paymentChannel = GrpcChannel.ForAddress("https://localhost:7031");
        var paymentClient = new PaymentService.PaymentService.PaymentServiceClient(paymentChannel);
        var inventoryResponse = await inventoryClient.CheckQuantityAsync(new Product()
        {
            ProductId = order.ProductId,
            Quantity = order.Quantity
        });
        var paymentResponse = await paymentClient.CheckUserBalanceAsync(new User()
        {
            UserId = order.CustomerId,
            OrderPrice = order.OrderPrice
        });
        if (!inventoryResponse.Available) 
            return new PostResponse() { Message = $"{inventoryResponse.Message}",Submitted = false};
        if (!paymentResponse.HasEnoughBalance)
            return new PostResponse() { Message = $"{paymentResponse.Message}",Submitted = false};
        
        return new PostResponse() { Message = "Order Has Been Submitted Successfully",Submitted = true};
    }
    }
