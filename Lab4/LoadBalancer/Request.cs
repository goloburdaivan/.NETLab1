namespace Lab4.LoadBalancer
{
    public class RequestBuilder
    {
        private const string HTTP_VERSION = "HTTP/1.0";
        private IDictionary<string, string> _headers = new Dictionary<string, string>();
        private string _requestMethod = "GET";
        private string _requestUri = "/";
        
        public RequestBuilder AddRequestMethod(string method)
        {
            _requestMethod = method;
            return this;
        }

        public RequestBuilder AddRequestUri(string uri) 
        {
            _requestUri = uri;
            return this;
        }

        public RequestBuilder AddHeader(string header, string value)
        {
            _headers.Add(header, value);
            return this;
        }

        public string Build()
        {
            string result = $"{_requestMethod} {_requestUri} {HTTP_VERSION}{Environment.NewLine}";
            foreach (var header in _headers)
            {
                result += $"{header.Key}: {header.Value}{Environment.NewLine}";
            }

            return result;
        }
    }
}
