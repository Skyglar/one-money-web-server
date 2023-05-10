using common.Attributes.DILifeTimeAttributes;
using common.ResponseBuilder.Contracts;

namespace common.ResponseBuilder {
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
