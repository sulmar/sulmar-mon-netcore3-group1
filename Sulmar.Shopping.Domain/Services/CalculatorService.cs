using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace Sulmar.Shopping.Domain.Services
{
    public class ProductCalculatorService
    {
        public decimal Calculate(Customer customer, decimal unitPrice)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer));

            if (unitPrice == 0)
                throw new ArgumentOutOfRangeException(nameof(unitPrice));

            switch (customer.City)
            {
                case "Warszawa":
                    return unitPrice * 2;
                case "Poznań":
                    return 0;
                default:
                    return unitPrice;
            }
        }
    }
}
