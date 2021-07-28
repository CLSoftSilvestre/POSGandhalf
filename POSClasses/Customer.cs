using System;
using System.Collections.Generic;

namespace POSClasses
{
    public class Customer : Person
    {
        public int VAT { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; }

        public Customer()
        {
        }
    }
}
