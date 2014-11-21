using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Payments
{
    public class CardDetails
    {
        private ITimeService timeService;

        public CardDetails(string number, string name, string expiryMonth, string expiryYear)
            :this(number, name, expiryMonth, expiryYear, new RealTimeService())
        {
        }

        public CardDetails(string number, string name, string expiryMonth, string expiryYear, ITimeService timeService)
        {
            Number = number;
            Name = name;
            ExpiryMonth = expiryMonth;
            ExpiryYear = expiryYear;
            this.timeService = timeService;
        }

        public string Number { get; private set; }
        public string Name { get; set; }
        public string ExpiryMonth { get; private set; }
        public string ExpiryYear { get; private set; }

        public void Validate()
        {
            if (!Number.All(c => char.IsNumber(c)))
            {
                throw new CardValidationException("Card number is not numeric");
            }

            if (!ExpiryYear.All(c => char.IsNumber(c)))
            {
                throw new CardValidationException("Expiry year is not numeric");
            }

            if (!ExpiryMonth.All(c => char.IsNumber(c)))
            {
                throw new CardValidationException("Expiry month is not numeric");
            }

            int month = int.Parse(ExpiryMonth);
            if (month < 1 || month > 12)
            {
                throw new CardValidationException("Expiry Month invalid");
            }

            int year = int.Parse(ExpiryYear);
            year += year > 89 ? 1900 : 2000;

            month++;
            if (month > 12)
            {
                month = 1;
                year++;
            }

            var cardExpiryDate = new DateTime(year, month, 1);

            if (timeService.Now >= cardExpiryDate)
            {
                throw new CardValidationException("Card has expired");
            }

        }
    }
}
