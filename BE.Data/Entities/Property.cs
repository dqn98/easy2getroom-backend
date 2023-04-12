using BE.Data.Enums;
using BE.Data.Interfaces;
using BE.Infrastructure.ShareKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE.Data.Entities
{
    [Table("Properties")]
    public class Property : DomainEntity<int>, ISwitchable, IDateTracking
    {
        public Property()
        {
        }

        public Property(int id, int propertyCategoryId, int wardsId, int rentalTypeId, string title, string desc, decimal price,
            decimal priceFrom, decimal priceTo, float acreage, float acreageFrom, float acreageTo, string address, double lat, double lng, Status status, Guid userId)
        {
            Id = id;
            PropertyCategoryId = propertyCategoryId;
            WardsId = wardsId;
            RentalTypeId = rentalTypeId;
            Title = title;
            Desc = desc;
            Price = price;
            PriceFrom = priceFrom;
            PriceTo = priceTo;
            Acreage = acreage;
            AcreageFrom = acreageFrom;
            AcreageTo = acreageTo;
            Address = address;
            Lat = lat;
            Lng = lng;
            Status = status;
            UserId = userId;
        }

        [Required]
        public int PropertyCategoryId { get; set; }

        [Required]
        public int WardsId { get; set; }

        [Required]
        public int RentalTypeId { get; set; }

        [StringLength(255)]
        [Required]
        public string Title { get; set; }

        [StringLength(1000)]
        [Required]
        public string Desc { get; set; }

        public decimal Price { get; set; }

        public decimal PriceFrom { get; set; }

        public decimal PriceTo { get; set; }

        public float Acreage { get; set; }

        public float AcreageFrom { get; set; }

        public float AcreageTo { get; set; }

        public string Address { get; set; }

        [DefaultValue("0")]
        public double Lat { get; set; }

        [DefaultValue("0")]
        public double Lng { get; set; }

        public int? Bedrooms { get; set; }

        public int? Bathrooms { get; set; }

        public int? Garages { get; set; }

        public bool Featured { get; set; }

        [DefaultValue(null)]
        public int? YearBuilt { get; set; }

        [DefaultValue("0")]
        public int Views { get; set; }

        [DefaultValue(0)]
        public decimal MonthlyWaterPrice { get; set; }

        [DefaultValue(0)]
        public decimal MonthlyElectricityPrice { get; set; }

        public Status Status { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public Guid UserId { get; set; }

        [ForeignKey("PropertyCategoryId")]
        public virtual PropertyCategory PropertyCategory { get; set; }

        [ForeignKey("WardsId")]
        public virtual Wards Wards { get; set; }

        [ForeignKey("RentalTypeId")]
        public virtual RentalType RentalType { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public virtual ICollection<PropertyImage> PropertyImages { get; set; }

        public virtual ICollection<PropertyFeature> PropertyFeatures { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Favorite> Favorites { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }
    }
}