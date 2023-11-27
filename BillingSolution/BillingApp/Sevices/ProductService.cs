
using BillingApp.Exceptions;
using BillingApp.Interfaces;
using BillingApp.Models;

namespace BillingApp.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<int, Product> _productRepository;

        public ProductService(IRepository<int, Product> repository)
        {
            _productRepository = repository;
        }
        public Product Add(Product product)
        {
            if (product.Price > 0)
            {
                var result = _productRepository.Add(product);
                return result;
            }
            return null;
        }

        public Product Delete(Product product)
        {
            var existingProduct = _productRepository.GetById(product.Id);
            if (existingProduct != null)
            {
                var deletedProduct = _productRepository.Delete(product.Id);
                return deletedProduct;
            }
            throw new ProductNotFoundException();
        }

        public Product Get(Product product)
        {
            var existingProduct = _productRepository.GetById(product.Id);
            if (existingProduct != null)
            {
                return existingProduct;
            }
            throw new ProductNotFoundException();
        }

        public List<Product> GetProducts()
        {
            var products = _productRepository.GetAll();
            if (products != null && products.Any())
            {
                return products.ToList();
            }
            throw new NoProductsAvailableException();
        }

        public Product Update(Product product)
        {
            var existingProduct = _productRepository.GetById(product.Id);
            if (existingProduct != null)
            {
                var updatedProduct = _productRepository.Update(product);
                return updatedProduct;
            }
            throw new ProductNotFoundException();
        }
    }
}
