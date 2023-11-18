using BillingApp.Interfaces;
using Microsoft.EntityFrameworkCore;
using BillingApp.Contexts;
using BillingApp.Models;


namespace BillingApp.Reposittories
{
    



    public class BillRepository : IRepository<int, Bill>
    {
        private readonly BillingContext _context;
        public BillRepository(BillingContext context)
        {
            _context = context;
        }
        public Bill Add(Bill entity)
        {
            _context.Bills.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public Bill Delete(int key)
        {
            var bill = GetById(key);
            if (bill != null)
            {
                _context.Bills.Remove(bill);
                _context.SaveChanges();
                return bill;
            }
            return null;
        }

        public IList<Bill> GetAll()
        {


            return _context.Bills.ToList();
        }

        public Bill GetById(int key)
        {
            var bill = _context.Bills.SingleOrDefault(u => u.billNumber == key);
            return bill;
        }

        public Bill Update(Bill entity)
        {
            var bill = GetById(entity.billNumber);
            if (bill != null)
            {
                _context.Entry<Bill>(bill).State = EntityState.Modified;
                _context.SaveChanges();
                return bill;
            }
            return null;
        }
    }
}

