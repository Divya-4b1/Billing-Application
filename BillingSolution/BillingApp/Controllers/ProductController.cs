using BillingApp.Exceptions;
using BillingApp.Interfaces;
using BillingApp.Models;


using BillingApp.Reposittories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BillingApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("reactApp")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public ActionResult Get()
        {
            string errorMessage = string.Empty;
            try
            {
                var result = _productService.GetProducts();
                return Ok(result);
            }
            catch (NoProductsAvailableException e)
            {
                errorMessage = e.Message;
            }
            return BadRequest(errorMessage);
        }
      //  [Authorize(Roles = "StoreManager")]
        [HttpPost]
        public ActionResult Create(Product product)
        {
            string errorMessage = string.Empty;
            try
            {
                var result = _productService.Add(product);
                return Ok(result);
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
            }
            return BadRequest(errorMessage);
        }
      //  [Authorize(Roles = "StoreManager")]
        [HttpPut("{id}")]
        public ActionResult Update(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest("Product ID in the URL does not match the ID in the request body.");
            }

            try
            {
                var updatedProduct = _productService.Update(product);
                return Ok(updatedProduct);
            }
            catch (ProductNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

      //  [Authorize(Roles = "StoreManager")]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var deletedProduct = _productService.Delete(new Product { Id = id });
                return Ok(deletedProduct);
            }
            catch (ProductNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}

