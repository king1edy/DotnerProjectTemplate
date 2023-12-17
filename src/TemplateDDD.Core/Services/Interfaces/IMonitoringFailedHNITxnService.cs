using TemplateDDD.Data.Dtos.MonitorFailedHNITxn;
using TemplateDDD.Shared.Responses;

namespace TemplateDDD.Core.Services.Interfaces
{
    public interface IMonitoringFailedHNITxnService
    {
        Task<ServiceResponse<MonitoringFailedHNITxnDto>> CreateAsync(CreateMonitoringFailedHNITxnDto model, string currentUserName, DateTime currentDateTime);

        Task<ServiceResponse<MonitoringFailedHNITxnDto>> UpdateAsync(Guid id, UpdateMonitoringFailedHNITxnDto model, string currentUserName, DateTime currentDateTime);

        Task<ServiceResponse<MonitoringFailedHNITxnDto>> DeleteAsync(Guid id);

        Task<ServiceResponse<MonitoringFailedHNITxnDto>> GetItemAsync(Guid id);

        Task<ServiceResponse<List<MonitoringFailedHNITxnDto>>> GetAllAsync();
    }
}