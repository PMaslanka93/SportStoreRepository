using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class NavController : Controller
    {
        //
        // GET: /Nav/
        IProductRepository repository;
        public NavController(IProductRepository rep)
        {
            repository = rep;
        }
        public PartialViewResult Menu(string category = null)
        {
            ViewBag.selectedCategory = category;
            IEnumerable<string> categories = repository.Products
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x);

            return PartialView(categories);
        }

    }
}
