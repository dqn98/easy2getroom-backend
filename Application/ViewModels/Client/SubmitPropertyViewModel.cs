using System;

namespace BE.Application.ViewModels.Client
{
    public class SubmitPropertyViewModel
    {
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public int PropertyCategoryId { get; set; }
        public int RentalTypeId { get; set; }
        public decimal Price { get; set; }
        public decimal PriceFrom { get; set; }
        public decimal PriceTo { get; set; }
        public float Acreage { get; set; }
        public float AcreageFrom { get; set; }
        public float AcreageTo { get; set; }
        public decimal MonthlyWaterPrice { get; set; }
        public decimal MonthlyElectricityPrice { get; set; }
        public string Address { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public int? BedRooms { get; set; }
        public int? BathRooms { get; set; }
        public int? Garages { get; set; }
        public int? YearBuild { get; set; }
        public int WardsId { get; set; }
    }
}