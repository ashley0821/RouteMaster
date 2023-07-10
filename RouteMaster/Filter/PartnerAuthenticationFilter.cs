using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc.Filters;
using System.Web.Mvc;
using System.Web.Routing;
using RouteMaster.Models.EFModels;
using System.Web.Security;

namespace RouteMaster.Filter
{
	public class PartnerAuthenticationFilter : ActionFilterAttribute, IAuthenticationFilter
	{
		public void OnAuthentication(AuthenticationContext filterContext)
		{
			//if (string.IsNullOrEmpty(Convert.ToString(filterContext.HttpContext.Session["UserName"])))
			//{
			//	filterContext.Result = new HttpUnauthorizedResult();
			//}
			if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
			{
				filterContext.Result = new HttpUnauthorizedResult();
			}
		}

		public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
		{
			if (filterContext.Result == null || filterContext.Result is HttpUnauthorizedResult)
			{
				//Redirecting the user to the Login View of Account Controller  
				filterContext.Result = new RedirectToRouteResult(
				new RouteValueDictionary
				{
					 { "controller", "Partners" },
					 { "action", "PartnerLogin" }
				});
			}
		}
		public class PartnerAuthorizeAttribute : AuthorizeAttribute
		{
			protected override bool AuthorizeCore(HttpContextBase httpContext)
			{
				bool authorize = false;
				var authCookie = httpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
				if (authCookie != null)
				{
					var ticket = FormsAuthentication.Decrypt(authCookie.Value);
					var userEmail = ticket.Name;
					using (var context = new AppDbContext())
					{
						var userRole = (from p in context.Partners
										where p.Email == userEmail
										select new
										{
											p.Id
										}).FirstOrDefault();
						if (userRole != null)
						{
							authorize = true;
						}
					}
					// 其他验证逻辑...
				}
				return authorize;

				//bool authorize = false;
				//var userEmail = Convert.ToString(httpContext.Session["UserEmail"]);
				//if (!string.IsNullOrEmpty(userEmail))
				//	using (var context = new AppDbContext())
				//	{
				//		var userRole = (from p in context.Partners
				//						where p.Email == userEmail
				//						select new
				//						{
				//							p.Id
				//						}).FirstOrDefault();
				//		if(userRole != null)
				//		{
				//				authorize = true;
				//		}
				//	}
				//return authorize;
			}

			protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
			{
				filterContext.Result = new RedirectToRouteResult(
				   new RouteValueDictionary
				   {
					{ "controller", "Home" },
					{ "action", "Index" }
				   });
			}
		}
	}
}