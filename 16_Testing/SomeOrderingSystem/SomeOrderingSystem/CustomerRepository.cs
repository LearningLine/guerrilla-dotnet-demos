using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeOrderingSystem
{
    public interface ICustomerRepository
    {
        double GetTaxRateForCustomer(int id);
    }

    public class CustomerRepository : ICustomerRepository
    {
        public double GetTaxRateForCustomer(int id)
        {
            return 0.1;
        }
    }
}
