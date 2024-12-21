using System;
using System.Web.Mvc;
using CompanyProject.Models;

namespace CompanyProject.Controllers
{
    public class BillingController : Controller
    {
        private readonly DBContext _context = new DBContext();

        public ActionResult Index()
        {
            var viewModel = new BillingViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult SaveBilling(BillingViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Invalid data. Please check the entered values.";
                return View("Index", viewModel);
            }

            try
            {
                // Save company details
                _context.CompanyDetails.Add(viewModel.Company);
                _context.SaveChanges();

                // Save product details
                foreach (var product in viewModel.Products)
                {
                    // Validate input values
                    if (product.Qty <= 0 || product.Rate <= 0)
                    {
                        TempData["ErrorMessage"] = "Quantity and Rate must be greater than zero.";
                        return View("Index", viewModel);
                    }

                    product.Basic = product.Qty * product.Rate;

                    // Handle potential arithmetic overflows
                    checked
                    {
                        product.SGSTAmt = product.Basic * 0.09m;
                        product.CGSTAmt = product.Basic * 0.09m;
                        product.TaxAmt = product.SGSTAmt + product.CGSTAmt;
                        product.GrandTotal = product.Basic + product.TaxAmt;
                    }

                    _context.ProductDetails.Add(product);
                }
                _context.SaveChanges();
            }
            catch (OverflowException ex)
            {
                TempData["ErrorMessage"] = "Arithmetic overflow occurred while calculating totals.";
                return View("Index", viewModel);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred: {ex.Message}";
                return View("Index", viewModel);
            }

            TempData["SuccessMessage"] = "Billing details saved successfully.";
            return RedirectToAction("Index");
        }
    }

}