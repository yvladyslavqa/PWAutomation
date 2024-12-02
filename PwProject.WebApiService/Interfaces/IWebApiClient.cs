namespace PwProject.WebApiService.Interfaces
{
    public interface IWebApiClient
    {
        public void SetBaseUrl(string? url);

        public void AddDefaultRequestHeaders(IDictionary<string, string> headers);

        public void SetCredentialsAuthorization(string? username, string? password);

        public void SetTokenAuthorization(string token);

        public HttpResponseMessage PostRequest(string requestUri, object content, string contentType = "application/json");

        public T PostRequest<T>(string requestUri, object content, string contentType = "application/json");

        public T PostRequest<T>(string requestUri, Dictionary<string, string> content);

        public HttpResponseMessage PostWithHeaderRequest(string requestUri, Dictionary<string, string> header, object content, string contentType);

        public HttpResponseMessage PostUrlEncodedRequest(string requestUri, Dictionary<string, string> content);

        public HttpResponseMessage GetRequest(string requestUri);

        public T GetRequest<T>(string requestUri);

        public HttpResponseMessage PostFile(string requestUri, byte[] file, string paramName, string fileName, string contentType);

        public T PostFile<T>(string requestUri, byte[] file, string paramName, string fileName, string contentType);
    }
}
