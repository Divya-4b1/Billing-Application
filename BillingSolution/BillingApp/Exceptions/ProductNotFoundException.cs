namespace BillingApp.Exceptions
{
    public class ProductNotFoundException : Exception
    {
        string message;
        public ProductNotFoundException()
        {
            message = "Product Not Found";
        }
        public override string Message => message;
    }
}


