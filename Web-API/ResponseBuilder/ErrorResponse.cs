using System.Net;
using common.ResponseBuilder.Contracts;

namespace one_money_web_server.ResponseBuilder {
    public class ErrorResponse : IWebResponse {
        public object Body { get; set; }

        public string Message { get; set; }

        public HttpStatusCode StatusCode { get; set; }
    }
}
