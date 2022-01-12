using Microsoft.AspNetCore.Mvc;
using System;

namespace common.WebApi
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class AssignActionRouteAttribute : RouteAttribute
    {
        public AssignActionRouteAttribute(string template) : base(template)
        {
        }
    }
}
