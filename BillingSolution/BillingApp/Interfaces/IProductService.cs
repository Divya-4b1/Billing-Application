using BillingApp.Models;

namespace BillingApp.Interfaces
{
    public interface IProductService
    {
        List<Product> GetProducts();
        Product Add(Product product);
    }
}