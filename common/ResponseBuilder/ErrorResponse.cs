using common.ResponseBuilder.Contracts;
using System.Net;

namespace common.ResponseBuilder {
    public class ErrorResponse : IWebResponse {
        public object Body { get; set; }

        public string Message { get; set; }

        public HttpStatusCode StatusCode { get; set; }
    }
}
