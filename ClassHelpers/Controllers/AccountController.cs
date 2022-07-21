using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClassHelpers.Repositories;
using ClassHelpers.Models.Domain;
using ClassHelpers.Models.InputModels;
using ClassHelpers.Models.BusinessLogic;
using System.Web.Security;
using Microsoft.AspNetCore.Mvc;



namespace ClassHelpers.Controllers
{
    public class AccountController : Controller
    {
        private IAccountRepository accountRepository = new AccountRepository();
        private IContactRepository contactRepository = new ContactRepository();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
               
                byte[] data = System.Text.Encoding.ASCII.GetBytes(model.Password);
                data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
                string password = System.Text.Encoding.ASCII.GetString(data);

               
                Account account = accountRepository.GetAccount(model.MobileNumber, password);
                if (account != null)
                {
                    FormsAuthentication.SetAuthCookie(account.MobileNumber, false);
                    Session["loggedin_account"] = account;
                    return RedirectToAction("Index", "Contact");
                }
                else ModelState.AddModelError("Login_error", "The mobile number or password provided is incorrect.");
            }
            else ModelState.AddModelError("", "The input provided is invalid.");
            return View(model); 
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                if (!accountRepository.CheckIfAccountExists(model.MobileNumber)) 
                {
                    Account account = new Account();
                    account.MobileNumber = model.MobileNumber;
                    account.FirstName = model.FirstName;
                    account.LastName = model.LastName;

                   
                    byte[] data = System.Text.Encoding.ASCII.GetBytes(model.Password);
                    data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
                    account.Password = System.Text.Encoding.ASCII.GetString(data);

                    
                    accountRepository.CreateAccount(account);

                   
                    contactRepository.AddOwnerAccountsToContacts(account.MobileNumber, account.AccountId);
                    return RedirectToAction("Login", "Account");
                }
                else ModelState.AddModelError("", "This mobile number already has an account. Try logging in with it!");
            }
            return View(model); 
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon(); 
            return RedirectToAction("Login", "Account");
        }
    }
}
