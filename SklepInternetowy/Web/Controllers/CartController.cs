using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class CartController : Controller
    {
        //
        // GET: /Cart/
        IProductRepository repository;
        private IOrderProcessor orderProcessor;
        private IProductRepository productRepository;
        public CartController(IProductRepository rep, IOrderProcessor proc)
        {
            repository = rep;
            orderProcessor = proc;
        }

       
        public RedirectToRouteResult AddToCart(Cart cart, int productId, string returnUrl)
        {
            Product product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);
            if (product != null) {cart.AddItem(product, 1); };
            return RedirectToAction("Index", new{returnUrl});

           
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId, string returnUrl)
        {
            Product product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);
            if (product != null) { cart.RemoveLine(product); }
            return RedirectToAction("Index", new { returnUrl });
        }     

        public ViewResult Index(Cart cart ,string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,   
                ReturnUrl = returnUrl,

            });
        
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }        

        [HttpPost]

        public ViewResult Checkout(Cart cart, ShoppingDetails details)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Koszyk Jest Pusty");
            }

            if (ModelState.IsValid)
            {
                orderProcessor.ProcessorOrder(cart, details);
                cart.clear();
                return View("Completed");
            }
            else
            {
                return View(details);
            }
        }

        public ViewResult Checkout()
        {
            return View(new ShoppingDetails());
        }
    }
}
