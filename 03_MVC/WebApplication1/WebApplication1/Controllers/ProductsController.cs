using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ProductsController : Controller
    {
        // ~/Products/Index
        //[Route("fred/kljdfhjdhf/{foo:int}")]
        //public void Index(string message, string bar, int foo=5)
        public ActionResult Index(ProductIndexModel model)
        {
            ViewData["the-message"] = model.Message;
            //ViewData.Model = model;
            return View("Index", model);

            //return new ViewResult
            //{
            //    ViewName = "Index"
            //};
            //return Content("this is text", "text/plain");

            //var redirect = new RedirectResult("http://google.com?q=" + model.Message);
            //return redirect;

            //var resp = new ContentResult()
            //{
            //    Content = "<data>Hello! The message was: "  + model.Message + "</data>",
            //    ContentType = "application/xml"
            //};
            //return resp;

            //var message = Request.QueryString["message"];
            //Response.StatusCode = 404;
            //Response.Write("<h1>The message (as a param) was: " +
            //    model.Message + "</h1>");
        }

        //[HttpGet]
        public ActionResult Edit(int id)
        {
            var model = new ProductViewModel
            {
                ID = id,
                Name = "Beer",
                Price = 12.5M
            };

            return View("Edit", model);
        }

        [HttpPost]
        public ActionResult Update(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            return View("Edit", model);
        }
    }

}