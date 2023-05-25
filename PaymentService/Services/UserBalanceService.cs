using Grpc.Core;

namespace PaymentService.Services;

public class UserBalanceService:PaymentService.PaymentServiceBase
{
    private readonly ILogger<UserBalanceService> _logger;

    private readonly List<UserDto> _users = new List<UserDto>()
    {
        new UserDto(){UserId = 1,Balance = 100},
        new UserDto(){UserId = 2,Balance = 600},
        new UserDto(){UserId = 3,Balance = 0},
        new UserDto(){UserId = 4,Balance = 1000},
    };

    public UserBalanceService(ILogger<UserBalanceService> _logger)
    {
        this._logger = _logger;
    }
    public override Task<response> CheckUserBalance(User request, ServerCallContext context)
    {
        _logger.LogInformation($"Request For UserId {request.UserId}");
        var user = _users.FirstOrDefault(user => user.UserId == request.UserId);
        if(user==null)
            return Task.FromResult<response>(new response(){Message = "User Not Found",HasEnoughBalance = false});
        if(user.Balance<request.OrderPrice)
            return Task.FromResult<response>(new response(){Message = "Insufficient Balance",HasEnoughBalance = false});
        
        return Task.FromResult<response>(new response(){Message = "Ok",HasEnoughBalance = true});

    }
}