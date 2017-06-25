using PlanetNewsAdmin.Models.Poco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PlanetNewsAdmin.Comman;
using System.Web.Security;

namespace PlanetNewsAdmin.Controllers
{
    public class HomeController : Controller
    {
     //   public object ServerBAL { get; private set; }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginReq model, string returnUrl = "")
        {
            // Lets first check if the Model is valid or not
            if (ModelState.IsValid)
            {
                string username = model.UserId;
                string password = model.Password;

                // Now if our password was enctypted or hashed we would have done the
                // same operation on the user entered password here, But for now
                // since the password is in plain text lets just authenticate directly
                var entities = ServerBAL.AuthenticateUser(model);

                // User found in the database
                if (entities.Success)
                {

                    FormsAuthentication.SetAuthCookie(username, model.RememberMe);
                    Session.Add("FName", entities.FirstName);
                    Session.Add("FullName", string.Join(" ", entities.FirstName, entities.LastName));
                    Session.Add("UserId", entities.UserRegistrationId);

                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            ModelState.Remove("password");
            return View(model);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "Home");
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}