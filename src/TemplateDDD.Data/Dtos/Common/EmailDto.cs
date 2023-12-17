namespace TemplateDDD.Data.Dtos.Common
{
    public class EmailDto
    {
        public Guid Id { get; set; }
        public string[]? EmailTo { get; set; }
        public string EmailFrom { get; set; } = string.Empty;
        public string EmailSubject { get; set; } = string.Empty;
        public string EmailBody { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public bool IsSent { get; set; }
    }
}