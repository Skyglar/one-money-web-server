namespace common.ResponseBuilder.Contracts {
    public interface IResponseFactory {
        IWebResponse GetSuccessReponse();

        IWebResponse GetErrorResponse();
    }
}
