using DocumentRepositoryApp.Models;
using System.Web.Mvc;
using System.Web.Security;

namespace DocumentRepositoryApp.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Account/

        public ActionResult Index()
        {
            return View();
        }

        //[AllowAnonymous]
        //public ActionResult Login(string returnUrl)
        //{
        //    ViewBag.ReturnUrl = returnUrl;
        //    return View();
        //}

        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public ActionResult Login(LoginModel model, string returnUrl)
        //{
        //    if (ModelState.IsValid && Membership.ValidateUser(model.Login, model.Password))
        //    {
        //        FormsAuthentication.SetAuthCookie(model.Login, createPersistentCookie: true);
        //        return RedirectToLocal(returnUrl);
        //    }

        //    ModelState.AddModelError("", "The user name or password provided is incorrect.");
        //    return View(model);
        //}

        //public ActionResult LogOff()
        //{
        //    FormsAuthentication.SignOut();

        //    return RedirectToAction("Login", "Account");
        //}


        //#region Helpers
        //private ActionResult RedirectToLocal(string returnUrl)
        //{
        //    if (Url.IsLocalUrl(returnUrl))
        //        return Redirect(returnUrl);
        //    else
        //        return RedirectToAction("Index", "Home");
        //}

        //#endregion
    }
}
