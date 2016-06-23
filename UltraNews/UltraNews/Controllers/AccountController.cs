using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using UltraNews.Models;
using UltraNews.Providers;

namespace UltraNews.Controllers
{

        public class AccountController : Controller
        {
            
            [HttpGet]
            public ActionResult Login()
            {
                return View();
            }



            [HttpPost]
            public ActionResult Login(LoginModel model, string returnUrl)
            {
                if (ModelState.IsValid)
                {
                    if (Membership.ValidateUser(model.Login, model.Password))
                    {
                        FormsAuthentication.SetAuthCookie(model.Login, model.RememberMe);
                        if (Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("List", "News");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Неправильный пароль или логин");
                    }
                }
                return View(model);
            }
            public ActionResult LogOff()
            {
                FormsAuthentication.SignOut();

                return RedirectToAction("Login", "Account");
            }

            public ActionResult Register()
            {
                return View(new RegisterModel());
            }

            [HttpPost]
            public ActionResult Register(RegisterModel model)
            {
                if (ModelState.IsValid)
                {
                    if (model.Password != model.RepeatPassword)
                    {
                        ModelState.AddModelError("", "Пароли не совпадают");
                        return View(model);
                    }


                    MembershipUser membershipUser = ((UltraMembershipProvider)Membership.Provider).CreateUser(model.Login, model.Password, model.Name, model.FamilyName, model.BirthDay);

                    if (membershipUser != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.Login, false);
                        return RedirectToAction("List", "News");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                    }
                }
                //что то не так ввел
                return View(model);
            }


            [Authorize]
            public ActionResult ShowProfile(int? id)
            {
                if (id == null)
                    return RedirectToAction("ShowProfile", new { id = new SqlCore().Users.Where(u => 
                        u.Login == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault().Id });

                User user = new SqlCore().Users.Where(u => u.Id == id).FirstOrDefault();
                if (user == null)
                    return HttpNotFound();
                return View(user);
            }
        
        }

    }
