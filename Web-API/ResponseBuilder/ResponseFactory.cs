using common.Attributes.DILifeTimeAttributes;
using common.ResponseBuilder;
using common.ResponseBuilder.Contracts;

namespace one_money_web_server.ResponseBuilder {
    [ScopedRegistration]
    public class ResponseFactory : IResponseFactory {
        public IWebResponse GetSuccessReponse() {
            return new SuccessResponse();
        }

        public IWebResponse GetErrorResponse() {
            return new ErrorResponse();
        }
    }
}
