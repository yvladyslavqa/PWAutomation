using System.Net.Http.Headers;
using System.Text;

namespace PwProject.WebApiService
{
    internal class HttpRequestMessageBuilder
    {
        private readonly string _url;
        private readonly HttpMethod _method;
        private Authentication.Authentication _authentication;
        private Dictionary<string, string> _headers = new();
        private HttpContent _contentBody;
        private FormUrlEncodedContent _urlContent;

        public HttpRequestMessageBuilder(HttpMethod method, string url)
        {
            _url = url;
            _method = method;
        }

        public HttpRequestMessageBuilder SetAuthorization(Authentication.Authentication authentication)
        {
            _authentication = authentication;
            return this;
        }

        public HttpRequestMessageBuilder SetHeaders(Dictionary<string, string> headers)
        {
            _headers = headers;
            return this;
        }

        public HttpRequestMessageBuilder SetContentBody(string contentBody, string contentType)
        {
            _contentBody = new StringContent(contentBody, Encoding.UTF8, contentType);
            return this;
        }

        public HttpRequestMessageBuilder SetUrlEncodedBuilder(Dictionary<string, string> contentBody)
        {
            _urlContent = new FormUrlEncodedContent(contentBody);
            return this;
        }

        public HttpRequestMessageBuilder SetFileContentBody(byte[] file, string paramName, string fileName, string contentType)
        {
            var boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");

            var multipartFormContent = new MultipartFormDataContent(boundary);
            var fileStreamContent = new StreamContent(new MemoryStream(file));
            fileStreamContent.Headers.ContentType = MediaTypeHeaderValue.Parse(contentType);
            fileStreamContent.Headers.Add("Content-Disposition", $"form-data; name={paramName}; filename={fileName}");

            multipartFormContent.Add(fileStreamContent);

            _contentBody = multipartFormContent;
            return this;
        }

        public HttpRequestMessage Build()
        {
            var message = new HttpRequestMessage(_method, _url);

            if (_authentication != null)
            {
                message.Headers.Authorization = new AuthenticationHeaderValue(_authentication.Type, _authentication.Value);
            }

            foreach (var header in _headers)
            {
                message.Headers.Add(header.Key, header.Value);
            }

            if (_contentBody != null)
            {
                message.Content = _contentBody;
            }

            if (_urlContent != null)
            {
                message.Content = _urlContent;
            }

            return message;
        }
    }
}
