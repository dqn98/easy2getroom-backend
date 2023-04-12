using BE.Data.Enums;
using System;
using System.Collections.Generic;

namespace BE.Application.ViewModels.Client
{
    public class PropertyVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public string PropertyType { get; set; }
        public List<string> PropertyStatus { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string FormattedAddress { get; set; }
        public Location Location { get; set; }
        public List<string> Features { get; set; }
        public bool Featured { get; set; }
        public Price Price { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public int Garages { get; set; }
        public Area Area { get; set; }
        public int YearBuilt { get; set; }
        public int RatingsCount { get; set; }
        public int RatingsValue { get; set; }
        public List<AdditionalFeature> AdditionalFeatures { get; set; }
        public List<Gallery> Gallery { get; set; }
        public DateTime Published { get; set; }
        public DateTime LastUpdate { get; set; }
        public int Views { get; set; }
        public int WardsId { get; set; }
        public int DistrictId { get; set; }
        public int CityId { get; set; }
        public decimal MonthlyWaterPrice { get; set; }
        public decimal MonthlyElectricityPrice { get; set; }
        public Status Status { get; set; }
    }
}