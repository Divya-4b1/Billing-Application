using BillingApp.Models.DTOs;

namespace BillingApp.Interfaces
{
    
    public interface IUserService
    {
        UserDTO Login(UserDTO userDTO);
        UserDTO Register(UserDTO userDTO);
    }
}
