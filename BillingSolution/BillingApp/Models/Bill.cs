using System.ComponentModel.DataAnnotations.Schema;

namespace BillingApp.Models
{
    public class Bill
    {
        public int billNumber { get; set; }
        public string Username { get; set; }

        [ForeignKey("Username")]
        public User User { get; set; }
    }
}
