using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace TemplateDDD.Shared.Responses
{
    [Serializable]
    public class ApiResponse<T> : BaseResponse
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public T data { get; private set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string[] messages { get; private set; }

        public ApiResponse(T value) : base(true)
        {
            data = value;
        }

        public ApiResponse(string[] value) : base(false)
        {
            messages = value;
        }

        public ApiResponse(ModelStateDictionary modelState) : base(false)
        {
            messages = modelState.Keys
                .SelectMany(key => modelState[key].Errors.Select(x => x.ErrorMessage))
                .ToArray();
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}