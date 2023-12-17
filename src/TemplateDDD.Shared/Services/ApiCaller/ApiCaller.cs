using System.Text;
using Newtonsoft.Json;
using TemplateDDD.Shared.Constants;

namespace TemplateDDD.Shared.Services.ApiCaller
{
    public class ApiCaller : IApiCaller
    {
        public async Task<string> GetAsync(string url, IDictionary<string, string> headers)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new Exception(ErrorConstants.ErrorMessage009);
            }

            string apiResponse = string.Empty;

            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromSeconds(60);

                if (headers != null)
                {
                    foreach (var item in headers)
                    {
                        httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }
                }

                using (var response = await httpClient.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode && response.StatusCode.ToString() == "OK")
                    {
                        apiResponse = await response.Content.ReadAsStringAsync();
                    }
                }
            }

            return apiResponse;
        }

        public async Task<string> PostAsync(string url, object content, IDictionary<string, string> headers)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new Exception(ErrorConstants.ErrorMessage009);
            }

            string apiResponse = string.Empty;

            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromSeconds(60);

                StringContent seriallizedContent = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, GeneralConstants.APPLICATION_CONTENT_TYPE_JSON);

                if (headers != null)
                {
                    foreach (var item in headers)
                    {
                        httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }
                }

                using (var response = await httpClient.PostAsync(url, seriallizedContent))
                {
                    if (response.IsSuccessStatusCode == false && response.StatusCode.ToString().ToLower() == "paymentrequired")
                    {
                        apiResponse = "paymentrequired";
                    }
                    else if (response.IsSuccessStatusCode && response.StatusCode.ToString().ToLower() == "ok")
                    {
                        apiResponse = await response.Content.ReadAsStringAsync();
                    }
                }
            }

            return apiResponse;
        }

        public async Task<TResult> PostAsync<TResult>(string url, List<KeyValuePair<string, string>> postData, IDictionary<string, string> headers, string headerAccept = "")
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new Exception(ErrorConstants.ErrorMessage009);
            }

            string apiResponse = string.Empty;

            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromSeconds(60);

                if (!string.IsNullOrEmpty(headerAccept))
                {
                    httpClient.DefaultRequestHeaders.Add("Accept", headerAccept);
                }

                using (var content = new FormUrlEncodedContent(postData))
                {
                    content.Headers.Clear();

                    if (headers != null)
                    {
                        foreach (var item in headers)
                        {
                            content.Headers.Add(item.Key, item.Value);
                        }
                    }

                    using (HttpResponseMessage response = await httpClient.PostAsync(url, content))
                    {
                        if (response.IsSuccessStatusCode && (response.StatusCode.ToString().ToLower() == "created" || response.StatusCode.ToString().ToLower() == "ok"))
                        {
                            apiResponse = await response.Content.ReadAsStringAsync();
                        }
                    }
                }
            }

            var dResponse = JsonConvert.DeserializeObject<TResult>(apiResponse);

            return dResponse;
        }
    }
}