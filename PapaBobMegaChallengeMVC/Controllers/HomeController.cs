using PapaBobMegaChallenge.Domain;
using PapaBobMegaChallenge.DTO;
using PapaBobMegaChallenge.Persistence.Repositories;
using PapaBobMegaChallengeMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;

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
            var totalAccountBalance = orderService.GetTotalAccountBalance(model.Customer.phone_number);
            if (model.payment_type == PaymentType.Cash)
            {
                ViewBag.Cost = cost;
                ViewBag.PreviousBalance = totalAccountBalance;
                ViewBag.TotalAccountBalance = totalAccountBalance;
            }
            else
            {
                ViewBag.Cost = cost;
                ViewBag.PreviousBalance = totalAccountBalance-cost;
                ViewBag.TotalAccountBalance = totalAccountBalance;
            }
            return View();
        }

        public ActionResult OrderManagement()
        {
            var orderService = new OrderService(new CustomerRepository(), new PizzaPriceTableRepository(), new OrderRepository());
            var listOfOrdersNotCompletedTemp = orderService.ObtainOrdersList();
            var listOfOrdersNotCompleted = new List<Order>();
            foreach (var order in listOfOrdersNotCompletedTemp)
            {
                listOfOrdersNotCompleted.Add(OrderMapper(order));
            }
            return View(listOfOrdersNotCompleted);
        }

        public ActionResult OrderCompleted(string modelOrderId)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }
            var orderService = new OrderService(new CustomerRepository(), new PizzaPriceTableRepository(), new OrderRepository());
            orderService.ChangeOrderStatus(modelOrderId);
            return RedirectToAction("OrderManagement", "Home");
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult UpdatePrice(Order model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }

            //JObject json = JObject.Parse(model);
            //var order = new JavaScriptSerializer().Deserialize<Order>(model);
            var orderService = new OrderService(new CustomerRepository(), new PizzaPriceTableRepository(), new OrderRepository());
            
            var cost = orderService.CalculateAmountOwing(OrderMapperWithoutCustomer(model));
            //const double cost = 12.0;
            //model.cost = decimal.Parse(cost.ToString());
            //JSONConvert --> JSONConvert.Serialize
            //var f = Json(cost.ToString());
            return Content(cost.ToString());
        }

        private static Order OrderMapper(OrderDto orderDto)
        {
            var order = new Order
            {
                order_id = orderDto.order_id,
                green_peppers = orderDto.green_peppers,
                sausage = orderDto.sausage,
                onions = orderDto.onions,
                pepperoni = orderDto.pepperoni,
                cost = orderDto.cost,
                size = (Size)orderDto.size,
                crust = (Crust)orderDto.crust,
                payment_type = (PaymentType)orderDto.payment_type,
                Customer = new Customer
                {
                    name = orderDto.Customer.name,
                    address = orderDto.Customer.address,
                    phone_number = orderDto.Customer.phone_number,
                    zip_code = orderDto.Customer.zip_code
                }
            };

            return order;
        }

        private static OrderDto OrderMapper(Order order)
        {
            var orderDto = new OrderDto
            {
                order_id = order.order_id,
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

        private static OrderDto OrderMapperWithoutCustomer(Order order)
        {
            var orderDto = new OrderDto
            {
                order_id = order.order_id,
                green_peppers = order.green_peppers,
                sausage = order.sausage,
                onions = order.onions,
                pepperoni = order.pepperoni,
                cost = order.cost,
                size = (PapaBobMegaChallenge.DTO.Enums.Size)order.size,
                crust = (PapaBobMegaChallenge.DTO.Enums.Crust)order.crust,
                payment_type = (PapaBobMegaChallenge.DTO.Enums.PaymentType)order.payment_type,
            };

            return orderDto;
        }

    }
}