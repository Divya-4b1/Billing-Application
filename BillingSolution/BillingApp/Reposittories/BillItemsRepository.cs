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
    }
}

    

