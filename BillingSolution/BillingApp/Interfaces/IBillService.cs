using BillingApp.Models.DTOs;
namespace BillingApp.Interfaces
{
    public interface IBillService
    {
        bool AddToBill(BillDTO billDTO);
        bool RemoveFromBill(BillDTO billDTO);
        decimal CalculateTotalBillAmount(int billNumber);
        string GenerateBillReceipt(int billNumber);
    }
}
