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

            var orderService = new OrderService(new CustomerRepository(), new PizzaPriceTableRepository(), new OrderRepository());
            var cost = orderService.CalculateAmountOwing(OrderMapper(model));
            model.cost = decimal.Parse(cost.ToString());
            orderService.AddOrder(OrderMapper(model));
            ViewBag.Cost = cost;
            return View();
        }

        private static OrderDto OrderMapper(Order order)
        {
            var orderDto = new OrderDto
            {
                green_peppers = order.green_peppers,
                sausage = order.sausage,
                onions = order.onions,
                pepperoni = order.pepperoni,
                cost = order.cost,
                size = (PapaBobMegaChallenge.DTO.Enums.Size)order.size,
                crust = (PapaBobMegaChallenge.DTO.Enums.Crust)order.crust,
                payment_type = (PapaBobMegaChallenge.DTO.Enums.PaymentType)order.payment_type,
                Customer = new CustomerDto
                {
                    name = order.Customer.name,
                    address = order.Customer.address,
                    phone_number = order.Customer.phone_number,
                    zip_code = order.Customer.zip_code
                }
            };

            return orderDto;
        }

    }
}