using Microsoft.AspNetCore.Mvc;
using TriviaNight.Models;
using TriviaNight;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Net;

namespace TriviaNight
{
    public class RequireLoginAttribute : ActionFilterAttribute
    {
        private readonly triviaDataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        
        public RequireLoginAttribute(triviaDataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            var httpContext = _httpContextAccessor.HttpContext;
            var controller = filterContext.Controller as Controller;

            if (httpContext.Request.Cookies.ContainsKey(Global.TokenCookieName))
            {
                string token = WebUtility.UrlDecode(_httpContextAccessor.HttpContext.Request.Cookies[Global.TokenCookieName]);
                User user = _context.User.First(u => u.Token.Equals(token));
                if (user.TokenExpires > DateTime.Now)
                {
                    controller.ViewData[Global.UserDataKey] = user;
                    return;
                }
            }
            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
                new
                {
                    action = "Login",
                    controller = "Accounts",
                    returnUrl = filterContext.HttpContext.Request.Path
                }
            ));
        }
    }
}
