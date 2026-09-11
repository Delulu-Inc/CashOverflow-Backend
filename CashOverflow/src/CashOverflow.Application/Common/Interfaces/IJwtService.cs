using CashOverflow.Domain.Entities;


namespace CashOverflow.Application.Common.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(ApplicationUser user);
    }
}
