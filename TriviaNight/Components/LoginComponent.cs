using Microsoft.AspNetCore.Mvc;
using TriviaNight.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace TriviaNight.Components
{
    public class LoginComponent : ViewComponent
    {
        private readonly triviaDataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginComponent(triviaDataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (_httpContextAccessor.HttpContext.Request.Cookies.ContainsKey(Global.TokenCookieName))
            {
                string token = WebUtility.UrlDecode(_httpContextAccessor.HttpContext.Request.Cookies[Global.TokenCookieName]);
                User user = _context.User.First(u => u.Token.Equals(token));
                if (user.TokenExpires > DateTime.Now)
                {
                    return View(user);
                }
            }
            return View(null);
        }
    }
}
