using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeOrderingSystem
{
    public class OrderResult
    {

        public double Tax { get; set; }

        public double Total { get; set; }
    }

    public class OrderService
    {
        public ICustomerRepository CustomerRepository { get; set; }
        public ITaxService TaxService { get; set; }

        public OrderService(ICustomerRepository customerRepo, ITaxService taxService)
        {
            CustomerRepository = customerRepo;
            TaxService = taxService;
        }

        public OrderResult PlaceOrder(int customerId, int qty, int proID)
        {
            if (qty <= 0)
            {
                return null;
            }

            var rate = CustomerRepository.GetTaxRateForCustomer(customerId);
            
            double tax = 0;
            if (rate >= 0)
            {
                tax = TaxService.Calc(100, rate);
            }

            return new OrderResult
            {
                Tax = tax,
                Total = 100 + tax
            };
        }
    }
}
