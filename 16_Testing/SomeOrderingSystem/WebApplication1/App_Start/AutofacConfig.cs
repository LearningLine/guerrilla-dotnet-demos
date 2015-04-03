using Autofac;
using Autofac.Integration.Mvc;
using SomeOrderingSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Controllers;

namespace WebApplication1.App_Start
{
    public class AutofacConfig
    {
        public static IContainer Register()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<TaxService>().As<ITaxService>();
            builder.RegisterType<CustomerRepository>().As<ICustomerRepository>();
            builder.RegisterType<OrderService>().AsSelf();

            //builder.Register(ctx =>
            //{
            //    var t = new TaxService();
            //    t.Foo = 5;
            //    return t;
            //}).As<ITaxService>();

            builder.RegisterControllers(typeof(AutofacConfig).Assembly);

            var container = builder.Build();
            return container;
        }
    }
}