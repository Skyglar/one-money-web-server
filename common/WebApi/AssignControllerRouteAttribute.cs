using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;

namespace common.WebApi
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class AssignControllerRouteAttribute : RouteAttribute
    {
        /// <summary>
        ///     Web Api Version.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        ///     Dev or release enevironment.
        /// </summary>
        public string Environment { get; set; }

        public AssignControllerRouteAttribute(string environment, int version, string template) :
            //base($"{environment}/{BuildRouteVersion(version)}/{{culture}}/{template}")
            base($"{environment}/{BuildRouteVersion(version)}/uk/{template}")
        {
            Version = BuildRouteVersion(version);
            Environment = environment;
        }

        private static string BuildRouteVersion(int number)
        {
            return $"v{number}";
        }
    }
}
