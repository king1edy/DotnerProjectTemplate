using Microsoft.AspNetCore.Mvc;
using TemplateDDD.Shared.Responses;

namespace TemplateDDD.APP.Common
{
    public class RootController : ControllerBase
    {
        public RootController()
        {
        }

        public DateTime CurrentDateTime
        {
            get
            {
                return DateTime.UtcNow;
            }
        }

        protected ApiResponse<T> ApiResponse<T>(T value)
        {
            return new ApiResponse<T>(value);
        }

        protected ApiResponse<T> ApiResponse<T>(string[] value)
        {
            return new ApiResponse<T>(value);
        }
    }
}