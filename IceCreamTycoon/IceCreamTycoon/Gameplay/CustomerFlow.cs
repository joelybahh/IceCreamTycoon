using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IceCreamTycoon
{
    class CustomerFlow
    {
        public int noOfCustomers;

        public CustomerFlow()
        {
            noOfCustomers = 0;
        }

        public int CalculateCustomerFlow()
        {
            noOfCustomers = (HUD.temperature / 6) + (HUD.popularity / 9);

            return noOfCustomers;
        }
    }
}
