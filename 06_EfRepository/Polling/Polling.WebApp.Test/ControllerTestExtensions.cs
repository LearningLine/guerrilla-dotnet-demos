using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Polling.WebApp.Test
{
    public static class ControllerTestExtensions
    {
        public static T AssertViewModel<T>(this ActionResult result, string viewName) where T : class
        {
            var viewResult = AssertViewResult(result, viewName);

            var model = viewResult.Model as T;
            Assert.IsNotNull(model);
            return model;
        }

        public static ViewResult AssertViewResult(ActionResult result, string viewName)
        {
            var viewResult = AssertResultType<ViewResult>(result);
            Assert.AreEqual(viewName, viewResult.ViewName);
            return viewResult;
        }

        public static T AssertResultType<T>(ActionResult result)
            where T : class
        {
            var viewResult = result as T;
            Assert.IsNotNull(viewResult);
            return viewResult;
        }

    }
}