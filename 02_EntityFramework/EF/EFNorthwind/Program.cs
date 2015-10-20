using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;

namespace EFNorthwind
{
    class Program
    {
        static void Main(string[] args)
        {
            //Func<string, int> foo = s => s.Length;

            //Expression<Func<string, int>> exprFoo = s => s.Length;


            //Console.WriteLine(foo);
            //Console.WriteLine(exprFoo);

            //var func = exprFoo.Compile();
            //Console.WriteLine(func("Scott thinks he rules"));



            using (var context = new NorthwindEntities())
            {
                //IQueryable<Customer> customers = context.Customers;
                //var value = "A";
                //query = query.Where(c => c.CompanyName.StartsWith(value));

                    //from c in context.Customers
                    //where c.CompanyName.StartsWith("A")
                    //orderby c.CompanyName descending
                    //select c;

                //ObjectResult<CustOrdersOrders_Result> something  = context.CustOrdersOrders()
                


                //Console.WriteLine(customers);

                foreach (var order in context.Orders.Include(o => o.Customer))
                {
                    Console.WriteLine(order.Customer.CompanyName);
                }
            }
        }
    }
}
