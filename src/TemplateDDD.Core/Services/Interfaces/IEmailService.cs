using TemplateDDD.Data.Dtos.Common;
using TemplateDDD.Shared.Responses;

namespace TemplateDDD.Core.Services.Interfaces
{
    public interface IEmailService
    {
        Task<ServiceResponse<bool>> SendEmail(EmailDto email);
    }
}