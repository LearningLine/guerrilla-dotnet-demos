using SomeOrderingSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class OrderController : Controller
    {
        OrderService orderService;

        public OrderController(OrderService orderService)
        {
            this.orderService = orderService;
        }

        // GET: Order
        public ActionResult Index()
        {
            return View();
        }
    }
}