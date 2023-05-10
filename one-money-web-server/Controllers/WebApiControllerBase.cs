using common.ResponseBuilder.Contracts;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System.Net;

namespace one_money_web_server.Controllers
{
    public abstract class WebApiControllerBase : Controller
    {
        private readonly IResponseFactory _responseFactory;

        /// <summary>
        ///     Nlogger
        /// </summary>
        protected Logger Logger { get; }

        /// <summary>
        ///     ctor().
        /// </summary>
        protected WebApiControllerBase(IResponseFactory responseFactory)
        {
            Logger = LogManager.GetCurrentClassLogger();

            _responseFactory = responseFactory;
        }

        protected IWebResponse SuccessResponseBody(object body, string message = "")
        {
            IWebResponse response = _responseFactory.GetSuccessReponse();

            response.Body = body;
            response.StatusCode = HttpStatusCode.OK;
            response.Message = message;

            return response;
        }

        protected IWebResponse ErrorResponseBody(string message, HttpStatusCode statusCode)
        {
            IWebResponse response = _responseFactory.GetErrorResponse();

            response.Message = message;
            response.StatusCode = statusCode;

            return response;
        }
    }
}
