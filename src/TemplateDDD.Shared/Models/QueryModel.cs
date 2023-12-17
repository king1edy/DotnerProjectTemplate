using TemplateDDD.Shared.Constants;

namespace TemplateDDD.Shared.Models
{
    public class BaseQueryModel
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class QueryModel : BaseQueryModel
    {
        public string? Filter { get; set; }
        public string? Keyword { get; set; }
    }

    public class ActiveQueryModel : QueryModel
    {
        public string ActiveStatus { get; set; } = ActiveStatuses.ACTIVE;
    }

    public class BaseSearchDateModel
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}