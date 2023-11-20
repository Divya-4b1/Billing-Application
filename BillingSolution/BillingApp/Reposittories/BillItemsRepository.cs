using BillingApp.Interfaces;
using Microsoft.EntityFrameworkCore;

using BillingApp.Contexts;

using BillingApp.Models;

namespace BillingApp.Reposittories
{
    public class BillItemsRepository : IRepository<int, BillItems>
    {
        private readonly BillingContext _context;
        public BillItemsRepository(BillingContext context)
        {
            _context = context;
        }

        public BillItems Add(BillItems entity)
        {
            _context.BillItems.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public BillItems Delete(int key)
        {
            var item = _context.BillItems.FirstOrDefault(ci => ci.Product_Id == key);
            if (item != null)
            {
                _context.BillItems.Remove(item);
                _context.SaveChanges();
                return item;
            }
            return null;
        }

        public IList<BillItems> GetAll()
        {

            return _context.BillItems.ToList();
        }

        public BillItems GetById(int key)
        {
            var item = _context.BillItems.FirstOrDefault(ci => ci.Product_Id == key);
            return item;
        }

        public BillItems Update(BillItems entity)
        {
            var bill = GetById(entity.Product_Id);
            if (bill != null)
            {
                _context.Entry<BillItems>(bill).State = EntityState.Modified;
                _context.SaveChanges();
                return bill;
            }
            return null;
        }
        public decimal CalculateTotalBillAmount(int billNumber)
        {
            var billItems = _context.BillItems.Where(b => b.BillNumber == billNumber).ToList();
            decimal totalAmount = 0;

            foreach (var item in billItems)
            {
                totalAmount += (decimal)item.Price * item.Quantity; // Explicitly cast item.Price from float to decimal
            }

            return totalAmount;
        }

        public string GenerateBillReceipt(int billNumber)
        {
            var billItems = _context.BillItems.Where(b => b.BillNumber == billNumber).ToList();
            decimal totalAmount = CalculateTotalBillAmount(billNumber);

            // Generate bill receipt content
            string receipt = $"Bill Number: {billNumber}\n";

            foreach (var item in billItems)
            {
                receipt += $"Product ID: {item.Product_Id}, Price: {item.Price}, Quantity: {item.Quantity}\n";
            }

            receipt += $"Total Amount: {totalAmount}\n";

            return receipt;
        }
    }
}

    

