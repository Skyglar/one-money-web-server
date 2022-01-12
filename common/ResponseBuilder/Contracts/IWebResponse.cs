using System.Net;

namespace common.ResponseBuilder.Contracts {
    public interface IWebResponse {
        object Body { get; set; }

        string Message { get; set; }

        HttpStatusCode StatusCode { get; set; }
    }
}
