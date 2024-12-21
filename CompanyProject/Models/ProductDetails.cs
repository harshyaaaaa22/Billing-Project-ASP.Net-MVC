using System;
using System.Web;

namespace CompanyProject.Models
{
    public class ProductDetails
    {
        public int ProductID { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ProductHSN { get; set; }
        public int Qty { get; set; }
        public decimal Rate { get; set; }
        public decimal SGSTAmt { get; set; }
        public decimal CGSTAmt { get; set; }
        public decimal Basic { get; set; }
        public decimal TaxAmt { get; set; }
        public decimal GrandTotal { get; set; }
    }
}
