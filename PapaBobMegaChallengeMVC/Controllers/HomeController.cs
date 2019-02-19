using PapaBobMegaChallenge.Domain;
using PapaBobMegaChallenge.DTO;
using PapaBobMegaChallenge.Persistence.Repositories;
using PapaBobMegaChallengeMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PapaBobMegaChallengeMVC.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult PlaceOrder(Order model)
        {
            if(!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }

            OrderService orderService = new OrderService(new CustomerRepository(), new PizzaPriceTableRepository(), new OrderRepository());
            double cost = orderService.CalculateAmountOwing(OrderMapper(model));
            ViewBag.Cost = cost;
            return View();
        }

        private OrderDto OrderMapper(Order order)
        {
            var orderDto = new OrderDto();
            orderDto.green_peppers = order.green_peppers;
            orderDto.sausage = order.sausage;
            orderDto.onions = order.onions;
            orderDto.pepperoni = order.pepperoni;
            orderDto.size = (PapaBobMegaChallenge.DTO.Enums.Size)order.size;
            orderDto.crust = (PapaBobMegaChallenge.DTO.Enums.Crust)order.crust;
            orderDto.payment_type = (PapaBobMegaChallenge.DTO.Enums.PaymentType)order.payment_type;
            orderDto.Customer = new CustomerDto();
            orderDto.Customer.name = order.Customer.name;
            orderDto.Customer.address = order.Customer.address;
            orderDto.Customer.phone_number = order.Customer.phone_number;
            orderDto.Customer.zip_code = order.Customer.zip_code;

            return orderDto;
        }

    }
}