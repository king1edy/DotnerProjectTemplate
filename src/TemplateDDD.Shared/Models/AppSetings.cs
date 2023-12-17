namespace TemplateDDD.Shared.Models
{
    internal class AppSetings
    {
        public Connectionstrings? ConnectionStrings { get; set; }
        public Appsettings? AppSettings { get; set; }
        public Serilog? Serilog { get; set; }
        public Hangfiresettings? HangfireSettings { get; set; }
        public HangFireSettings? HangFireSettings { get; set; }
        public Logging? Logging { get; set; }
    }

    public class Connectionstrings
    {
        public string DefaultConnection { get; set; } = string.Empty;
    }

    public class MailSettings
    {
        public string? Mail { get; set; } = string.Empty;
        public string[]? MailTo { get; set; }
        public string? DisplayName { get; set; } = string.Empty;
        public string? Password { get; set; } = string.Empty;
        public string? Host { get; set; } = string.Empty;
        public int Port { get; set; }
        public bool UseSSl { get; set; }
        public bool UseDefaultCredentials { get; set; }
    }

    public class EtherealMailSettings
    {
        public string? Mail { get; set; } = string.Empty;
        public string[]? MailTo { get; set; }
        public string? DisplayName { get; set; } = string.Empty;
        public string? Password { get; set; } = string.Empty;
        public string? Host { get; set; } = string.Empty;
        public int Port { get; set; }
        public bool UseSSl { get; set; }
        public bool UseDefaultCredentials { get; set; }
    }

    public class Appsettings
    {
        public string[]? AllowedCORSOrigins { get; set; }
        public MailSettings? MailSettings { get; set; }
        public EtherealMailSettings? EtherealMailSettings { get; set; }
        public Emailconfigsetting? EmailConfigSetting { get; set; }
        public int batchcount { get; set; }

        public string cronJob { get; set; }
        public int EmailServerType { get; set; }
    }

    public class Emailconfigsetting
    {
        public string EmailId { get; set; } = string.Empty;
        public string EmailFrom { get; set; } = string.Empty;
        public string[]? EmailTo { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }
        public string Security { get; set; } = string.Empty;
        public bool UseSSL { get; set; }
    }

    public class Serilog
    {
        public Minimumlevel MinimumLevel { get; set; }
        public Filter[] Filter { get; set; }
        public Writeto[] WriteTo { get; set; }
        public string[] Enrich { get; set; }
        public Properties Properties { get; set; }
    }

    public class Minimumlevel
    {
        public string Default { get; set; }
        public Override Override { get; set; }
    }

    public class Override
    {
        public string Microsoft { get; set; }
        public string MicrosoftHostingLifetime { get; set; }
    }

    public class Properties
    {
        public string ApplicationName { get; set; }
    }

    public class Filter
    {
        public string Name { get; set; }
        public Args Args { get; set; }
    }

    public class Args
    {
        public string expression { get; set; }
    }

    public class Writeto
    {
        public string Name { get; set; }
        public Args1 Args { get; set; }
    }

    public class Args1
    {
        public string path { get; set; }
        public string rollingInterval { get; set; }
        public string outputTemplate { get; set; }
        public string formatter { get; set; }
    }

    public class HangFireSettings
    {
        public string? CRONScheduler { get; set; }
        public int RetryAttempts { get; set; }
        public int SchedulePollingInterval { get; set; }
    }

    public class Hangfiresettings
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class Logging
    {
        public Loglevel loglevel { get; set; }
    }

    public class Loglevel
    {
        public string _default { get; set; }
        public string microsoftaspnetcore { get; set; }
    }
}