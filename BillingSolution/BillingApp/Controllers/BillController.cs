﻿using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BillingApp.Interfaces;
using BillingApp.Models.DTOs;

namespace BillingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private readonly IBillService _billService;

        public BillController(IBillService billService)
        {
            _billService = billService;
        }
        [HttpPost]
        public IActionResult AddToCart(BillDTO billDTO)
        {
            var result = _billService.AddToBill(billDTO);
            if (result)
                return Ok(billDTO);
            return BadRequest("Could not add item to cart");
        }
        [HttpPost]
        [Route("Remove")]
        public IActionResult RemoveFromCart(BillDTO billDTO)
        {
            var result = _billService.RemoveFromBill(billDTO);
            if (result)
                return Ok(billDTO);
            return BadRequest("Could not remove item from cart");
        }
    }
}

    