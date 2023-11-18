using System.ComponentModel.DataAnnotations;

namespace BillingApp.Models.DTOs
{
    public class BillDTO
    {
        [Required(ErrorMessage = "Username is empty")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Product Id is empty")]
        public int ProductId { get; set; }
        public int Quantity { get; set; } = 1;
    }
}
