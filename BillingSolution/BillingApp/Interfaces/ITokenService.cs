using BillingApp.Models.DTOs;

namespace BillingApp.Interfaces
{
    public interface ITokenService
    {
        string GetToken(UserDTO user);
    }
}
