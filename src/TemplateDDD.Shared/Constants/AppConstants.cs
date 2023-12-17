namespace TemplateDDD.Shared.Constants
{
    public class ApplicationConstants
    {
        public const string AppSettingsKey = "AppSettings";
        public const string AllowedCORSOrigins = "AppSettings:AllowedCORSOrigins";

        public const string EmailConfigSetting = "appSettings:EmailConfigSetting";

        public const string ConnectionStringsSectionKey = "ConnectionStrings";
        public const string DefaultConnectionSectionKey = "DefaultConnection";
        public const string DefaultConnectionStringKey = "ConnectionStrings:DefaultConnection";
        public const string CORSOriginsKey = "AppSettings:AllowedCORSOrigins";
    }

    public static class GeneralConstants
    {
        public const int PROCESSING_BATCH_SIZE = 10;

        public const string DEFAULT_COUNTRY_CODE = "+234";

        public const string APPLICATION_CONTENT_TYPE_JSON = "application/json";
    }

    public static class DateFormatConstants
    {
        public const string FULL = "yyyy-MM-dd hh:mm:ss tt";
    }

    public static class ActiveStatuses
    {
        public static readonly string ACTIVE = "ACTIVE";
        public static readonly string INACTIVE = "INACTIVE";
        public static readonly string ALL = "ALL";
    }

    public class ApprovalStatuses
    {
        public static readonly string PENDING = "PENDING";
        public static readonly string APPROVED = "APPROVED";
        public static readonly string REJECTED = "REJECTED";
        public static readonly string REPRESENT = "REPRESENT";
    }
}