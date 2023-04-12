using BE.Application.Interfaces;
using BE.Application.ViewModels.Client;
using BE.Data.Entities;
using BE.Data.Enums;
using BE.Data.IRepositories;
using BE.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE.Application.Implementations
{
    public class ClientPropertyService : IClientPropertyService
    {
        private IPropertyRepository _propertyRepository;
        private IUnitOfWork _unitOfWork;
        private UserManager<User> _userManager;

        public ClientPropertyService(IPropertyRepository propertyRepository, IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _propertyRepository = propertyRepository;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<List<PropertyVM>> GetFeaturedProperties()
        {
            var properties = new List<PropertyVM>();

            var propertiesFromDb = await _propertyRepository.FindAll(p => p.Wards.District.City, p => p.PropertyCategory,
                p => p.RentalType, p => p.PropertyImages, p => p.Ratings).Include(p => p.PropertyFeatures).ThenInclude(pf => pf.Feature)
                .Where(p => p.Featured == true || p.Status == Status.Active).OrderBy(p => p.DateCreated).OrderByDescending(p => p.DateCreated).ToListAsync();

            foreach (Property p in propertiesFromDb)
            {
                var vm = new PropertyVM();
                vm = MappingData(p);
                properties.Add(vm);
            }

            return properties;
        }

        public async Task<List<PropertyVM>> GetProperties()
        {
            var properties = new List<PropertyVM>();

            var propertiesFromDb = await _propertyRepository.FindAll(p => p.Wards.District.City, p => p.PropertyCategory,
                p => p.RentalType, p => p.PropertyImages, p => p.Ratings).Where(p => p.Status == Status.Active).Include(p => p.PropertyFeatures).ThenInclude(pf => pf.Feature)
                .OrderByDescending(p => p.DateCreated)
                .ToListAsync();
            foreach (Property p in propertiesFromDb)
            {
                var vm = new PropertyVM();
                vm = MappingData(p);
                properties.Add(vm);
            }

            return properties;
        }

        public async Task<PropertyVM> GetPropertyById(int id)
        {
            var property = new PropertyVM();
            var p = await _propertyRepository.FindAll(p => p.Wards.District.City, p => p.PropertyCategory,
                p => p.RentalType, p => p.PropertyImages, p => p.Ratings).Where(p => p.Status == Status.Active)
                .Include(p => p.PropertyFeatures).ThenInclude(pf => pf.Feature).Where(p => p.Id == id).FirstOrDefaultAsync();

            property = MappingData(p);

            return property;
        }

        public async Task<PropertyVM> GetPropertyByIdToEdit(int id)
        {
            var property = new PropertyVM();
            var p = await _propertyRepository.FindAll(p => p.Wards.District.City, p => p.PropertyCategory,
                p => p.RentalType, p => p.PropertyImages, p => p.Ratings)
                .Include(p => p.PropertyFeatures)
                .ThenInclude(pf => pf.Feature)
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();

            property = MappingData(p);

            return property;
        }

        public async Task<List<PropertyVM>> GetRelatedProperties(int id)
        {
            var property = await _propertyRepository.FindAll(p => p.Wards.District.City, p => p.PropertyCategory,
                 p => p.RentalType, p => p.PropertyImages, p => p.Ratings).Where(p => p.Status == Status.Active).Include(p => p.PropertyFeatures).ThenInclude(pf => pf.Feature).FirstOrDefaultAsync(p => p.Id == id);

            var relatedProperties = await _propertyRepository.FindAll(p => p.Wards.District.City, p => p.PropertyCategory,
                 p => p.RentalType, p => p.PropertyImages).Where(p => p.Wards.Name.Equals(property.Wards.Name)
                 || (p.Price == 0) ? true : (p.Price < (property.Price + 1000000) || p.Price > (property.Price))
                 || ((p.PriceFrom == 0) ? true : (p.PriceFrom >= property.PriceFrom) && (p.PriceTo == 0) ? true : (p.PriceTo <= property.PriceTo))).Where(p => p.Status == Status.Active)
                 .OrderByDescending(p => p.DateCreated)
                 .Take(8)
                 .ToListAsync();

            var properties = new List<PropertyVM>();
            foreach (Property p in relatedProperties)
            {
                if (p.Id != id)
                {
                    var vm = new PropertyVM();
                    vm = MappingData(p);
                    properties.Add(vm);
                }
            }

            return properties;
        }

        public PropertyVM MappingData(Property p)
        {
            var vm = new PropertyVM();

            vm.Id = p.Id;
            vm.Title = p.Title;
            vm.Desc = p.Desc;
            vm.PropertyType = p.PropertyCategory.Name;

            vm.PropertyStatus = new List<string>();
            vm.PropertyStatus.Add(p.RentalType.Name);

            vm.City = p.Wards.District.City.Name;
            vm.ZipCode = "";
            vm.FormattedAddress = p.Address + ", " + p.Wards.Name + ", " + p.Wards.District.Name + ", " + p.Wards.District.City.Name;

            vm.Location = new Location();
            vm.Location.Lat = p.Lat;
            vm.Location.Lng = p.Lng;

            vm.Features = new List<string>();
            if (p.PropertyFeatures != null)
            {
                foreach (PropertyFeature pf in p.PropertyFeatures)
                {
                    vm.Features.Add(pf.Feature.Name);
                }
            }
            vm.Featured = p.Featured;

            vm.Price = new Price();
            vm.Price.PriceFrom = p.PriceFrom;
            vm.Price.PriceTo = p.PriceTo;
            vm.Price.PriceFor = p.Price;

            vm.MonthlyWaterPrice = p.MonthlyWaterPrice;
            vm.MonthlyElectricityPrice = p.MonthlyElectricityPrice;

            if (p.Bedrooms != null)
            {
                vm.Bedrooms = p.Bedrooms.Value;
            }
            if (p.Bathrooms != null)
            {
                vm.Bathrooms = p.Bathrooms.Value;
            }

            if (p.Garages != null)
            {
                vm.Garages = p.Garages.Value;
            }

            vm.Area = new Area();
            vm.Area.Value = p.Acreage;
            vm.Area.ValueFrom = p.AcreageFrom;
            vm.Area.ValueTo = p.AcreageTo;
            vm.Area.Unit = "m2";

            vm.WardsId = p.WardsId;
            vm.DistrictId = p.Wards.DistrictId;
            vm.CityId = p.Wards.District.CityId;

            if (p.YearBuilt != null)
            {
                vm.YearBuilt = p.YearBuilt.Value;
            }

            if (p.Ratings != null)
            {
                vm.RatingsCount = p.Ratings.Count();
                vm.RatingsValue = 0;
                foreach (Rating rating in p.Ratings)
                {
                    vm.RatingsValue = vm.RatingsValue + rating.Value;
                }
            }
            else
            {
                vm.RatingsValue = 0;
                vm.RatingsCount = 1;
            }

            vm.Gallery = new List<Gallery>();
            foreach (PropertyImage pi in p.PropertyImages)
            {
                var gallery = new Gallery();

                gallery.Big = pi.Url;
                gallery.Medium = pi.Url;
                gallery.Small = pi.Url;

                vm.Gallery.Add(gallery);
            }

            vm.AdditionalFeatures = new List<AdditionalFeature>();
            vm.AdditionalFeatures.AddRange(new List<AdditionalFeature>()
                {
                    new AdditionalFeature() {Name= "Heat", Value ="Heat"}
                });

            vm.Published = p.DateCreated;
            vm.LastUpdate = p.DateModified;
            vm.Views = p.Views;
            vm.Status = p.Status;

            return vm;
        }

        public async Task<bool> AddViewCount(int propertyId)
        {
            var property = _propertyRepository.FindById(propertyId);
            property.Views = property.Views + 1;
            _propertyRepository.Update(property);
            return await _unitOfWork.CommitAllAsync();
        }

        public async Task<ClientUserViewModel> GetPropertyOwner(int propertyId)
        {
            var property = await _propertyRepository.FindAll(p => p.User).Where(p => p.Id == propertyId).FirstOrDefaultAsync();
            if (property.User == null)
            {
                return null;
            }
            var user = property.User;

            return MappingUser(user);
        }

        public ClientUserViewModel MappingUser(User user)
        {
            var vm = new ClientUserViewModel();
            vm.Id = user.Id;
            vm.FullName = user.FullName;
            vm.Desc = "Address: " + user.Address + ", Date registed: " + user.DateCreated.ToString();
            vm.Organization = "Easy2GetRoom";
            vm.Email = user.Email;
            vm.Phone = user.PhoneNumber;
            vm.Username = user.UserName;
            vm.Social = new Social();
            vm.Social.Facebook = user.FacebookUrl;
            vm.Social.Twitter = user.TwitterUrl;
            vm.Social.Website = user.WebsiteUrl;
            vm.Social.Instagram = "";
            vm.Social.Linkedin = "";

            vm.Image = user.Avatar;

            return vm;
        }

        public async Task<List<PropertyVM>> GetPropertiesByUsername(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            var properties = new List<PropertyVM>();

            var propertiesFromDb = await _propertyRepository.FindAll(p => p.Wards.District.City, p => p.PropertyCategory,
                p => p.RentalType, p => p.PropertyImages, p => p.Ratings).Where(p => p.Status == Status.Active && p.UserId == user.Id).Include(p => p.PropertyFeatures).ThenInclude(pf => pf.Feature).ToListAsync();
            foreach (Property p in propertiesFromDb)
            {
                var vm = new PropertyVM();
                vm = MappingData(p);
                properties.Add(vm);
            }

            return properties;
        }
    }
}