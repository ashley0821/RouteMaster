using Microsoft.SqlServer.Server;
using RouteMaster.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;
using System.Web.Security;

namespace RouteMaster.Filter
{
	public class AdministratorAuthenticationFilter : ActionFilterAttribute, IAuthenticationFilter
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
					 { "controller", "Administrators" },
					 { "action", "Login" }
				});
			}
		}

		public class CustomAuthorizeAttribute : AuthorizeAttribute
		{
			private readonly string[] allowedroles;
			public CustomAuthorizeAttribute(params string[] roles)
			{
				this.allowedroles = roles;
			}
			protected override bool AuthorizeCore(HttpContextBase httpContext)
			{
				bool authorize = false;
				var authCookie = httpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
				//var userEmail = Convert.ToString(httpContext.Session["UserEmail"]);
				if (authCookie != null)
				//if (!string.IsNullOrEmpty(userEmail))
					//using (var context = new AppDbContext())
					{
					var ticket = FormsAuthentication.Decrypt(authCookie.Value);
					var userEmail = ticket.Name;
					var identity = ticket.UserData;

					using (var context = new AppDbContext())
					{
						if(identity == "住所夥伴")
						{
							var partnerRole = (from p in context.Partners
											where p.Email == userEmail
											select new
											{
												p.Id
											}).FirstOrDefault();
							if (partnerRole != null)
							{
								authorize = true;
							}

						}
						if(identity == "總管理員")
						{

							var userRole = (from a in context.Administrators
												join p in context.Permissions on a.PermissionId equals p.Id
												where a.Email == userEmail
												select new
												{
													p.Name
												}).FirstOrDefault();
								foreach (var permission in allowedroles)
								{
									if (permission == userRole.Name)
									{

										authorize = true;
									};
								}
							}
                        if(identity == "會員")
                        {
                            var partnerRole = (from m in context.Partners
                                               where m.Email == userEmail
                                               select new
                                               {
                                                   m.Id
                                               }).FirstOrDefault();
                            if (partnerRole != null)
                            {
                                authorize = true;
                            }

                        }
                    }

					
				}


				return authorize;
			}

			protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
			{
				filterContext.Result = new RedirectToRouteResult(
				   new RouteValueDictionary
				   {
					{ "controller", "Administrators" },
					{ "action", "Login" }
				   });
			}
		}

	}
}