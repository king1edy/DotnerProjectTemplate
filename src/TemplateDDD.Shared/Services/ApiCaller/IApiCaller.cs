namespace TemplateDDD.Shared.Services.ApiCaller
{
    public interface IApiCaller
    {
        Task<string> GetAsync(string url, IDictionary<string, string> headers);

        Task<string> PostAsync(string url, object content, IDictionary<string, string> headers);

        Task<TResult> PostAsync<TResult>(string url, List<KeyValuePair<string, string>> postData, IDictionary<string, string> headers, string headerAccept);
    }
}