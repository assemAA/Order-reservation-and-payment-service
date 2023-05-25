using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Net.Client;
using InventoryService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using OrderServiceAPI.DTOs;
using PaymentService;

namespace OrderServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderDto order)
        {
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
                return BadRequest($"{inventoryResponse.Message}");
            if (!paymentResponse.HasEnoughBalance)
                return BadRequest($"{paymentResponse.Message}");
            
            return Ok(new {message="Order Submitted Successfully"});
        }
    }
}
