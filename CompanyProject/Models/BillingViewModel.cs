using System;
using System.Collections.Generic;
using System.Web;

namespace CompanyProject.Models
{

    public class BillingViewModel
    {
        public CompanyDetails Company { get; set; }
        public List<ProductDetails> Products { get; set; }

        public BillingViewModel()
        {
            Products = new List<ProductDetails>();
        }
    }
}
