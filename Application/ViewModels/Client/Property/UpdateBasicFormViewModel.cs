namespace BE.Application.ViewModels.Client.Property
{
    public class UpdateBasicFormViewModel
    {
        public string Title { get; set; }
        public string Desc { get; set; }
        public int RentalTypeId { get; set; }
        public int PropertyCategoryId { get; set; }
        public decimal Price { get; set; }
        public decimal PriceFrom { get; set; }
        public decimal PriceTo { get; set; }
        public float Acreage { get; set; }
        public float AcreageFrom { get; set; }
        public float AcreageTo { get; set; }
    }
}