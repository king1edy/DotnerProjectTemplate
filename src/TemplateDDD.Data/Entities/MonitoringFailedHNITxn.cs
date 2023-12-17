using TemplateDDD.Shared.Abstractions;

namespace TemplateDDD.Data.Entities
{
    public class MonitoringFailedHNITxn : Entity
    {
        public string AccountName { get; set; } = string.Empty;
        public string AccountNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string UserMessage { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string PhoneNo { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; }
    }
}