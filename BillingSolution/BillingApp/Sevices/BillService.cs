using BillingApp.Interfaces;
using BillingApp.Models;
using Microsoft.CodeAnalysis;

using BillingApp.Models.DTOs;
using BillingApp.Reposittories;

namespace BillingApp.Sevices;

public class BillSevice : IBillService
{
    private readonly IRepository<int, Bill> _billRepository;
    private readonly IRepository<int, BillItems> _billItemRepository;
    private readonly IRepository<int, Product> _productRepository;

    public BillSevice(IRepository<int, Bill> billRepository,
        IRepository<int, BillItems> billItemRepository,
        IRepository<int, Product> productRepository)
    {
        _billRepository = billRepository;
        _billItemRepository = billItemRepository;
        _productRepository = productRepository;
    }
    BillItems CreateBillItem(int billNumber, BillDTO billDTO)
    {
        var product = _productRepository.GetById(billDTO.ProductId);
        if (product != null)
        {
            var myBillItem = new BillItems
            {
                BillNumber = billNumber,
                Product_Id = billDTO.ProductId,
                Quantity = billDTO.Quantity,
                Price = product.Price
            };
            return myBillItem;
        }
        return null;
    }
    public bool AddToBill(BillDTO billDTO)
    {

        var billCheck = _billRepository.GetAll().FirstOrDefault(c => c.Username == billDTO.Username);
        int billNumber = 0;
        if (billCheck == null)
        {
            var bill = _billRepository.Add(new Bill { Username = billDTO.Username });
            billNumber = bill.billNumber;
        }
        else
            billNumber = billCheck.billNumber;
        bool BillItemCheck = CheckIfBillItemAlreadyPresent(billNumber, billDTO.ProductId);
        if (BillItemCheck)
        {
            return IncrementQuantityInBill(billNumber, billDTO);
        }
        var myBillItem = CreateBillItem(billNumber, billDTO);
        if (myBillItem != null)
        {
            var result = _billItemRepository.Add(myBillItem);
            if (result != null)
                return true;
        }
        return false;
    }

    private bool IncrementQuantityInBill(int billNumber, BillDTO billDTO)
    {
        var billItem = _billItemRepository.GetAll()
            .FirstOrDefault(ci => ci.BillNumber == billNumber && ci.Product_Id == billDTO.ProductId);
        billItem.Quantity += billDTO.Quantity;
        var result = _billItemRepository.Update(billItem);
        if (result != null)
            return true;
        return false;
    }

    private bool CheckIfBillItemAlreadyPresent(int billNumber, int productId)
    {
        var cartItem = _billItemRepository.GetAll()
            .FirstOrDefault(ci => ci.BillNumber == billNumber && ci.Product_Id == productId);
        return cartItem != null ? true : false;
    }

    private bool DecrementQuantityInBill(int billNumber, BillDTO billDTO)
    {
        var billItem = _billItemRepository.GetAll()
            .FirstOrDefault(ci => ci.BillNumber == billNumber && ci.Product_Id == billDTO.ProductId);
        billItem.Quantity -= billDTO.Quantity;
        int ProductId = billDTO.ProductId;
        if (billItem.Quantity == 0)
        {
            var result1 = _billItemRepository.Delete(ProductId);
            if (result1 != null)
                return true;
        }
        else if (billItem.Quantity < 0)
        {
            return false;
        }
        var result = _billItemRepository.Update(billItem);
        if (result != null)
            return true;
        return false;
    }

    public bool RemoveFromBill(BillDTO billDTO)
    {
        int billNumber = 0;
        var billCheck = _billRepository.GetAll().FirstOrDefault(c => c.Username == billDTO.Username);
        if (billCheck == null)
        {
            return false;
        }
        else
        {
            billNumber = billCheck.billNumber;
        }
        bool CartItemCheck = CheckIfBillItemAlreadyPresent(billNumber, billDTO.ProductId);
        if (CartItemCheck)
        {
            return DecrementQuantityInBill(billNumber, billDTO);
        }
        return false;
    }
}