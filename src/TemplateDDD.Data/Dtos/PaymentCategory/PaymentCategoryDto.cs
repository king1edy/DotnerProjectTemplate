namespace TemplateDDD.Data.Dtos.PaymentCategory
{
    public class PaymentCategoryDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}