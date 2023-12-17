using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace TemplateDDD.Shared.Responses
{
    [Serializable]
    public class ServiceResponse<T>
    {
        public List<ValidationResult> ValidationErrors { get; set; } = new List<ValidationResult>();

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public T Data { get; set; } = default(T);

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> ErrorMessages
        {
            get
            {
                var result = ValidationErrors.Select(c => c.ErrorMessage).ToList();
                if (result == null || result.Count == 0)
                {
                    return null;
                }
                else
                {
                    return result;
                }
            }
        }

        public List<string> ModeStateErrors(ModelStateDictionary modelState)
        {
            var result = modelState.Keys
                    .SelectMany(key => modelState[key].Errors.Select(x => x.ErrorMessage))
                    .ToList();

            if (result == null || result.Count == 0)
            {
                return null;
            }
            else
            {
                return result;
            }
        }

        public string? Message { get; set; }

        public string this[string columnName]
        {
            get
            {
                var validatioResult = ValidationErrors.FirstOrDefault(r => r.MemberNames.FirstOrDefault() == columnName);
                return validatioResult == null ? string.Empty : validatioResult.ErrorMessage;
            }
        }

        public bool HasError
        {
            get
            {
                if (ValidationErrors.Count > 0)
                {
                    return true;
                }

                return false;
            }
        }

        public void AddError(string error)
        {
            ValidationErrors.Add(new ValidationResult(error));
        }

        public void AddError(ValidationResult validationResult)
        {
            ValidationErrors.Add(validationResult);
        }

        public void AddError(IEnumerable<ValidationResult> validationResults)
        {
            ValidationErrors.AddRange(validationResults);
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}