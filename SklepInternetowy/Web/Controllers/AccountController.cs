using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Infrastructure.Abstract;
using Web.Models;

namespace Web.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/
        IAuthProvider prov;
        public AccountController(IAuthProvider p)
        {
            prov = p;
        }
        public ViewResult Login()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (prov.Authenticate(model.UserName, model.Password))
                {
                    return RedirectToAction(returnUrl ?? Url.Action("Index", "Admin"));
                }
                else
                {
                    ModelState.AddModelError("", "Zła nazwa uż albo hesesełło");
                    return View();
                }
                
            }
            else
            {
                return View();
            }
        }

    }
}
