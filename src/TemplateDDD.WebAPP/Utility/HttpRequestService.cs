using RestSharp;
using RestSharp.Authenticators;

namespace TemplateDDD.APP.Utility
{
    public class HttpRequestService
    {
        /// <summary>
        /// creates get requests
        /// </summary>
        /// <param name="url"></param>
        /// <returns>client response</returns>
        public async Task<RestResponse> GetRequest(string url, Dictionary<string, string> header, string parameter)
        {
            try
            {
                var options = new RestClientOptions(url) { MaxTimeout = -1 };
                //var client = new RestClient(options, parameter == null ? url : $"{url}?{parameter}");
                var client = new RestClient(options);
                var request = new RestRequest();
                //client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("Cache-Control", "no-cache");
                request.AddHeader("Accept", "*/*");
                if (header != null)
                    foreach (var head in header)
                    {
                        request.AddHeader(head.Key, head.Value);
                    }
                RestResponse response = await Task.FromResult(client.Execute(request));

                return response;
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// creates post requests
        /// </summary>
        /// <param name="url"></param>
        /// <param name="jsonData"></param>
        /// <returns>client response</returns>
        public async Task<RestResponse> PostRequest(string url, Dictionary<string, string> header, string parameter, string body)
        {
            try
            {
                var options = new RestClientOptions(url) { MaxTimeout = -1 };
                var client = new RestClient(options);

                var request = new RestRequest();
                //request.AddHeader("cache-control", "no-cache");
                //request.AddHeader("Cache-Control", "no-cache");
                request.AddHeader("Accept", "*/*");

                if (header != null)
                    foreach (var head in header)
                    {
                        request.AddHeader(head.Key, head.Value);
                    }

                request.AddStringBody(body, contentType: ContentType.Json);
                RestResponse response = await Task.FromResult(client.ExecutePost(request));

                return response;
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// creates post requests
        /// </summary>
        /// <param name="url"></param>
        /// <param name="jsonData"></param>
        /// <returns>client response</returns>
        public async Task<RestResponse> NewPostRequest(string url, Dictionary<string, string> header, string parameter, object body)
        {
            try
            {
                var options = new RestClientOptions(url) { MaxTimeout = -1 };
                var client = new RestClient(options);

                var request = new RestRequest();
                request.AddHeader("Accept", "*/*");
                request.AlwaysMultipartFormData = true;

                if (header != null)
                    foreach (var head in header)
                    {
                        request.AddHeader(head.Key, head.Value);
                    }

                request.AddObject(body);

                RestResponse response = await Task.FromResult(client.ExecutePost(request));

                return response;
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// creates put requests
        /// </summary>
        /// <param name="url"></param>
        /// <param name="jsonData"></param>
        /// <returns>client response</returns>
        public async Task<RestResponse> PutRequest(string url, Dictionary<string, string> header, string parameter, string body)
        {
            try
            {
                var client = new RestClient(parameter == null ? url : $"{url}?{parameter}");
                var request = new RestRequest("", Method.Put);
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("Cache-Control", "no-cache");
                request.AddHeader("Accept", "*/*");
                if (header != null)
                    foreach (var head in header)
                    {
                        request.AddHeader(head.Key, head.Value);
                    }
                request.AddParameter("undefined", body, ParameterType.RequestBody);
                RestResponse response = await Task.FromResult(client.Execute(request));

                return response;
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// creates delete requests
        /// </summary>
        /// <param name="url"></param>
        /// <returns>client response</returns>
        public async Task<RestResponse> DelRequest(string url, Dictionary<string, string> header, string parameter)
        {
            try
            {
                var client = new RestClient(parameter == null ? url : $"{url}?{parameter}");
                var request = new RestRequest("", Method.Delete);
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("Cache-Control", "no-cache");
                request.AddHeader("Accept", "*/*");
                if (header != null)
                    foreach (var head in header)
                    {
                        request.AddHeader(head.Key, head.Value);
                    }
                RestResponse response = await Task.FromResult(client.Execute(request));

                return response;
            }
            catch (Exception) { throw; }
        }

        public async Task<RestResponse> GetTokenAsync()
        {
            string url = EndpointDefinition.CREATE_TOKEN;
            var options = new RestClientOptions(url)
            {
                MaxTimeout = -1,
                // Only where needed...
                Authenticator = new HttpBasicAuthenticator("username", "password")
            };
            var client = new RestClient(options);
            var request = new RestRequest("", Method.Post);
            
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

            request.AddParameter("scope", "profile");
            request.AddParameter("grant_type", "client_credentials");
            RestResponse response = await client.ExecuteAsync(request);

            Console.WriteLine(response.Content);

            return response;
        }
    }
}