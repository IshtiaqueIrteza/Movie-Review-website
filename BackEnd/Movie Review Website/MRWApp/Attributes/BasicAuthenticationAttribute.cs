﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Net.Http;
using System.Web.Http.Filters;
using System.Text;
using System.Threading;
using System.Security.Principal;

namespace MRWApp.Attributes
{
    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            base.OnAuthorization(actionContext);

            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden);
            }
            else
            {
                // dXNlcjp1c2Vy
                string encodedString = actionContext.Request.Headers.Authorization.Parameter;

                // user:user
                string decodedString = Encoding.UTF8.GetString(Convert.FromBase64String(encodedString));


                string[] arr = decodedString.Split(new char[] { ':' });
                string username = arr[0];
                string password = arr[1];

                if ((username == "user" && password == "user") || (username == "admin" && password == "admin"))
                {
                    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(username), null);
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }
            }
        }
    }
}