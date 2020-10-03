using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TriviaNight.Models;
using Microsoft.AspNetCore.Mvc;
using BC = BCrypt.Net.BCrypt;
using System.IO;

namespace TriviaNight.Controllers
{
    public class AccountsController : Controller
    {
        private readonly triviaDataContext _context;

        public AccountsController(triviaDataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl = "/Home/Index")
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [ServiceFilter(typeof(RequireLoginAttribute))]
        public async Task<IActionResult> MyAccount()
        {
            return View(ViewData[Global.UserDataKey]);
        }

        [HttpPost]
        public async Task<IActionResult> DoLogin(string user, string password, string returnUrl)
        {
            user = user.ToLower();
            User me = _context.User.FirstOrDefault(
                u => u.DisplayName.ToLower().Equals(user) || u.Email.ToLower().Equals(user));
            if (me == null)
            {
                ViewData["Message"] = "Could not find an account for that Username or Email. Please try again";
                return View("Login");
            }
            if (BC.Verify(password, me.Password))
            {
                string token = BC.HashPassword($"{user}|{password}|{DateTime.Now.ToBinary()}");
                me.Token = token;
                me.TokenExpires = DateTime.Now.AddDays(90);
                me.LastLogon = DateTime.Now;
                me.LastIp = HttpContext.Connection.RemoteIpAddress.ToString();
                HttpContext.Response.Cookies.Append(Global.TokenCookieName, token);
                _context.User.Update(me);
                await _context.SaveChangesAsync();
                return Redirect(returnUrl);
            }
            else
            {
                ViewData["Message"] = "Invalid password. Please try again.";
                return View("Login");
            }
        }
        
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string username, string email, string password, string dob)
        {
            if (_context.User.Where(u => u.DisplayName.ToLower().Equals(username.ToLower())).Count() > 0)
            {
                ViewData["message"] = "A user with that username already exists.";
                return View();
            }

            if (_context.User.Where(u => u.Email.ToLower().Equals(email.ToLower())).Count() > 0)
            {
                ViewData["message"] = "That e-mail is associated with an existing account.";
                return View();
            }
            DateTime birthday;
            if (!DateTime.TryParse(dob, out birthday))
            {
                ViewData["Message"] = "Invalid birthday";
                return View();
            }
            string token = BC.HashPassword($"{username}|{password}|{DateTime.Now.ToBinary()}");
            _context.User.Add(
                new User()
                {
                    DisplayName = username,
                    Email = email,
                    Password = BC.HashPassword(password),
                    Birthday = birthday,
                    LastIp = HttpContext.Connection.RemoteIpAddress.ToString(),
                    Token = token,
                    TokenExpires = DateTime.Now.AddDays(90),
                    LastLogon = DateTime.Now,
                    CreateDate = DateTime.Now,
                    AvatarImage = "/images/avatars/default.png"
                });
            await _context.SaveChangesAsync();
            HttpContext.Response.Cookies.Append(Global.TokenCookieName, token);
            return RedirectToAction("ThankYou", "Accounts");
        }
        
        public async Task<IActionResult> Logout()
        {
            HttpContext.Response.Cookies.Delete(Global.TokenCookieName);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult ThankYou()
        {
            return View();
        }
    }
}
