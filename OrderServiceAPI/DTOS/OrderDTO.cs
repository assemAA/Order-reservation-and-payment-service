namespace OrderServiceAPI.DTOs;

public record OrderDto(int CustomerId,int ProductId,int Quantity,int OrderPrice);