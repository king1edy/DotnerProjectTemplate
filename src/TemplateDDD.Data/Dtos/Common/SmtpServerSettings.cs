namespace TemplateDDD.Data.Dtos.Common
{
    public class SmtpServerSettings
    {
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }
        public string Mail { get; set; } = string.Empty;
        public string MailTo { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public bool UseSSl { get; set; }
        public bool UseDefaultCredentials { get; set; }
    }
}