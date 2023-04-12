using BE.Application.Interfaces;
using BE.Application.ViewModels.Client;
using BE.Application.ViewModels.Client.Property;
using BE.Application.ViewModels.Client.Rating;
using BE.Application.ViewModels.Client.User;
using BE.Data.Entities;
using BE.Data.Enums;
using BE.Data.IRepositories;
using BE.Infrastructure.Interfaces;
using BE.Ultilities.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE.Application.Implementations
{
    public class AccountService : IAccountService
    {
        private IPropertyRepository _propertyRepository;
        private IPropertyImageRepository _propertyImageRepository;
        private ICommentRepository _commentRepository;
        private IFavoriteRepository _favoriteRepository;
        private IRatingRepository _ratingRepository;
        private IPropertyFeatureRepository _propertyFeatureRepository;
        private UserManager<User> _userManager;
        private IUnitOfWork _unitOfWork;

        public AccountService(IPropertyRepository propertyRepository,
            IPropertyImageRepository propertyImageRepository,
            ICommentRepository commentRepository,
            IFavoriteRepository favoriteRepository,
            IRatingRepository ratingRepository,
            IPropertyFeatureRepository propertyFeatureRepository,
            UserManager<User> userManager,
            IUnitOfWork unitOfWork)
        {
            _propertyRepository = propertyRepository;
            _propertyImageRepository = propertyImageRepository;
            _commentRepository = commentRepository;
            _favoriteRepository = favoriteRepository;
            _ratingRepository = ratingRepository;
            _propertyFeatureRepository = propertyFeatureRepository;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<bool> Favorite(FavoriteViewModel viewModel)
        {
            var isFavoriteExist = _favoriteRepository.FindAll().Where(f => f.UserId == viewModel.UserId && f.PropertyId == viewModel.propertyId).AsNoTracking().FirstOrDefault();
            if (isFavoriteExist != null)
            {
                return false;
            }
            var favorite = new Favorite()
            {
                UserId = viewModel.UserId,
                PropertyId = viewModel.propertyId
            };

            _favoriteRepository.Add(favorite);
            return await _unitOfWork.CommitAllAsync();
        }

        public async Task<bool> Unfavorite(FavoriteViewModel viewModel)
        {
            var isFavoriteExist = _favoriteRepository.FindAll().Where(f => f.UserId == viewModel.UserId && f.PropertyId == viewModel.propertyId).AsNoTracking().FirstOrDefault();
            if (isFavoriteExist == null)
            {
                return false;
            }
            var favorite = new Favorite()
            {
                UserId = viewModel.UserId,
                PropertyId = viewModel.propertyId
            };

            _favoriteRepository.Remove(favorite);
            return await _unitOfWork.CommitAllAsync();
        }

        public async Task<List<PropertyVM>> GetFavorites(Guid userId)
        {
            var properties = new List<PropertyVM>();

            var propertiesFromDb = await _propertyRepository.FindAll(p => p.Wards.District.City, p => p.PropertyCategory,
                p => p.RentalType, p => p.PropertyImages, p => p.Ratings).Include(p => p.Favorites).ThenInclude(f => f.User).ToListAsync();
            foreach (Property p in propertiesFromDb)
            {
                foreach (Favorite f in p.Favorites)
                {
                    if (f.UserId == userId)
                    {
                        var vm = new PropertyVM();
                        vm = MappingData(p);
                        properties.Add(vm);
                    }
                }
            }

            return properties;
        }

        public async Task<List<PropertyVM>> GetUserProperties(Guid userId)
        {
            var properties = new List<PropertyVM>();

            var propertiesFromDb = await _propertyRepository.FindAll(p => p.Wards.District.City, p => p.PropertyCategory,
                p => p.RentalType, p => p.PropertyImages).Where(p => p.UserId == userId).ToListAsync();
            foreach (Property p in propertiesFromDb)
            {
                var vm = new PropertyVM();
                vm = MappingData(p);
                properties.Add(vm);
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

            vm.MonthlyElectricityPrice = p.MonthlyElectricityPrice;
            vm.MonthlyWaterPrice = p.MonthlyWaterPrice;

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
                    new AdditionalFeature() {Name= "None", Value ="None"}
                });

            vm.Published = p.DateCreated;
            vm.LastUpdate = p.DateModified;
            vm.Views = p.Views;
            vm.Status = p.Status;

            return vm;
        }

        public async Task<List<PropertyVM>> GetMyProperties(GetUserPropertiesViewModel viewModel)
        {
            var properties = new List<PropertyVM>();

            var propertiesFromDb = await _propertyRepository.FindAll(p => p.Wards.District.City, p => p.PropertyCategory,
                p => p.RentalType, p => p.PropertyImages).Include(p => p.PropertyFeatures).ThenInclude(pf => pf.Feature)
                .Where(p => p.UserId == viewModel.UserId)
                .ToListAsync();
            foreach (Property p in propertiesFromDb)
            {
                var vm = new PropertyVM();
                vm = MappingData(p);
                properties.Add(vm);
            }

            return properties;
        }

        public async Task<bool> Rating(RatingViewModel viewModel)
        {
            var ratingFromDb = _ratingRepository.FindAll().Where(r => r.UserId == viewModel.UserId && r.PropertyId == viewModel.PropertyId).AsNoTracking().FirstOrDefault();
            if (ratingFromDb != null)
            {
                ratingFromDb.Value = viewModel.Value;
                _ratingRepository.Update(ratingFromDb);
            }
            else
            {
                var rating = new Rating()
                {
                    UserId = viewModel.UserId,
                    PropertyId = viewModel.PropertyId,
                    Value = viewModel.Value
                };

                _ratingRepository.Add(rating);
            }

            return await _unitOfWork.CommitAllAsync();
        }

        public async Task<int> SubmitProperty(SubmitPropertyViewModel viewModel)
        {
            var property = new Property();
            property.UserId = viewModel.UserId;
            property.Title = viewModel.Title;
            property.Desc = viewModel.Desc;
            property.PropertyCategoryId = viewModel.PropertyCategoryId;
            property.RentalTypeId = viewModel.RentalTypeId;
            if (viewModel.RentalTypeId == CommonConstants.NeedRentId || viewModel.RentalTypeId == CommonConstants.NeedSharingId)
            {
                property.Price = 0;
                property.PriceFrom = viewModel.PriceFrom;
                property.PriceTo = viewModel.PriceTo;

                property.Acreage = 0;
                property.AcreageFrom = viewModel.AcreageFrom;
                property.AcreageTo = viewModel.AcreageTo;
            }
            else
            {
                property.Price = viewModel.Price;
                property.PriceFrom = 0;
                property.PriceTo = 0;

                property.Acreage = viewModel.Acreage;
                property.AcreageFrom = 0;
                property.AcreageTo = 0;
            }

            property.MonthlyWaterPrice = viewModel.MonthlyWaterPrice;
            property.MonthlyElectricityPrice = viewModel.MonthlyElectricityPrice;

            property.Address = viewModel.Address;
            property.Lat = viewModel.Lat;
            property.Lng = viewModel.Lng;
            property.WardsId = viewModel.WardsId;
            property.Status = Status.AwaitingApproval;
            property.Featured = false;
            if (viewModel.BedRooms != null)
            {
                property.Bedrooms = viewModel.BedRooms;
            }
            if (viewModel.BathRooms != null)
            {
                property.Bathrooms = viewModel.BathRooms;
            }
            if (viewModel.Garages != null)
            {
                property.Garages = viewModel.Garages;
            }
            if (viewModel.YearBuild != null)
            {
                property.YearBuilt = viewModel.YearBuild;
            }

            _propertyRepository.Add(property);
            var isSuccess = await _unitOfWork.CommitAllAsync();
            if (isSuccess == true)
            {
                return property.Id;
            }
            return 0;
        }

        public async Task<bool> AddPropertyFeatures(AddPropertyFeaturesViewModel viewModel)
        {
            if (viewModel.Features.Count != 0)
            {
                for (int i = 0; i < viewModel.Features.Count(); i++)
                {
                    var propertyFeature = new PropertyFeature();
                    propertyFeature.PropertyId = viewModel.PropertyId;
                    propertyFeature.FeatureId = viewModel.Features[i];
                    _propertyFeatureRepository.Add(propertyFeature);
                }
                return await _unitOfWork.CommitAllAsync();
            }
            return false;
        }

        public async Task<bool> UpdateStatus(ClientUpdateStatusViewModel viewModel)
        {
            var property = _propertyRepository.FindById(viewModel.Id);
            if (property.UserId == viewModel.UserId)
            {
                property.Status = viewModel.Status;
                _propertyRepository.Update(property);
                return await _unitOfWork.CommitAllAsync();
            }
            return false;
        }

        public async Task<bool> DeleteProperty(DeletePropertyViewModel viewModel)
        {
            var property = await _propertyRepository.FindAll(p => p.PropertyFeatures, p => p.Ratings, p => p.Favorites, p => p.Comments, p => p.PropertyImages)
                .Where(p => p.Id == viewModel.PropertyId)
                .FirstOrDefaultAsync();
            if (property != null)
            {
                if (property.Ratings.Count() != 0)
                {
                    _ratingRepository.RemoveMultiple(property.Ratings.ToList());
                    await _unitOfWork.CommitAllAsync();
                }

                if (property.PropertyFeatures.Count() != 0)
                {
                    _propertyFeatureRepository.RemoveMultiple(property.PropertyFeatures.ToList());
                    await _unitOfWork.CommitAllAsync();
                }

                if (property.Favorites.Count() != 0)
                {
                    _favoriteRepository.RemoveMultiple(property.Favorites.ToList());
                    await _unitOfWork.CommitAllAsync();
                }

                if (property.Comments.Count() != 0)
                {
                    _commentRepository.RemoveMultiple(property.Comments.ToList());
                    await _unitOfWork.CommitAllAsync();
                }

                if (property.PropertyImages.Count() != 0)
                {
                    _propertyImageRepository.RemoveMultiple(property.PropertyImages.ToList());
                    await _unitOfWork.CommitAllAsync();
                }
            }
            _propertyRepository.Remove(property);
            return await _unitOfWork.CommitAllAsync();
        }

        public async Task<int> CheckRating(CheckRatingViewModel viewModel)
        {
            var rate = await _ratingRepository.FindAll().Where(r => r.PropertyId == viewModel.PropertyId && r.UserId == viewModel.UserId).FirstOrDefaultAsync();
            if (rate != null)
            {
                return rate.Value;
            }
            return 0;
        }

        public async Task<ProfileViewModel> GetProfile(string username)
        {
            var vm = new ProfileViewModel();

            var userFromDb = await _userManager.FindByNameAsync(username);

            vm.Address = userFromDb.Address;
            vm.Birthday = userFromDb.BirthDay;
            vm.Email = userFromDb.Email;
            vm.Facebook = userFromDb.FacebookUrl;
            //vm.Image = userFromDb.Avatar;
            vm.Name = userFromDb.FullName;
            vm.Phone = userFromDb.PhoneNumber;
            vm.Twitter = userFromDb.TwitterUrl;
            vm.Website = userFromDb.WebsiteUrl;
            vm.Avatar = userFromDb.Avatar;
            vm.AvatarPublicId = userFromDb.AvatarPublicId;

            return vm;
        }

        public async Task<bool> UpdateProfile(UpdateProfileViewModel viewModel)
        {
            var user = await _userManager.FindByIdAsync(viewModel.UserId.ToString());
            if (user != null)
            {
                user.FullName = viewModel.Name;
                user.FacebookUrl = viewModel.Facebook;
                user.TwitterUrl = viewModel.Twitter;
                user.PhoneNumber = viewModel.Phone;
                user.Address = viewModel.Address;
                user.BirthDay = viewModel.Birthday;
                user.WebsiteUrl = viewModel.Website;

                var isSuccess = await _userManager.UpdateAsync(user);
                if (isSuccess.Succeeded)
                {
                    return true;
                }
            }

            return false;
        }

        public async Task<bool> UpdateAvatar(string id, string avatar, string avatarPublicId)
        {
            var user = await _userManager.FindByIdAsync(id);

            user.Avatar = avatar;
            user.AvatarPublicId = avatarPublicId;

            var isSuccess = await _userManager.UpdateAsync(user);
            if (isSuccess.Succeeded)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateBasicForm(UpdateBasicFormViewModel viewModel, int propertyId)
        {
            var property = await _propertyRepository.FindByIdAsync(propertyId);
            property.Title = viewModel.Title;
            property.Desc = viewModel.Desc;
            property.RentalTypeId = viewModel.RentalTypeId;
            property.PropertyCategoryId = viewModel.PropertyCategoryId;
            property.Price = viewModel.Price;
            property.PriceFrom = viewModel.PriceFrom;
            property.PriceTo = viewModel.PriceTo;
            property.Acreage = viewModel.Acreage;
            property.AcreageFrom = viewModel.AcreageFrom;
            property.AcreageTo = viewModel.AcreageTo;

            _propertyRepository.Update(property);
            return await _unitOfWork.CommitAllAsync();
        }

        public async Task<bool> UpdateAddressForm(UpdateAddressFormViewModel viewModel, int propertyId)
        {
            var property = await _propertyRepository.FindByIdAsync(propertyId);
            property.Address = viewModel.Address;
            property.WardsId = viewModel.WardsId;
            property.Lat = viewModel.Lat;
            property.Lng = viewModel.Lng;
            _propertyRepository.Update(property);
            return await _unitOfWork.CommitAllAsync();
        }

        public async Task<bool> UpdateAdditionalForm(UpdateAdditionalFormViewModel viewModel, int propertyId)
        {
            var property = await _propertyRepository.FindByIdAsync(propertyId);
            var propertyFeatures = await _propertyFeatureRepository.FindAll().Where(f => f.PropertyId == propertyId).ToListAsync();
            if (propertyFeatures.Count() != 0)
            {
                _propertyFeatureRepository.RemoveMultiple(propertyFeatures);
                var result = await _unitOfWork.CommitAllAsync();
                if (result == true)
                {
                    if (viewModel.Features.Count() != 0)
                    {
                        var newFeatures = new List<PropertyFeature>();
                        foreach (int id in viewModel.Features)
                        {
                            var feature = new PropertyFeature();
                            feature.PropertyId = propertyId;
                            feature.FeatureId = id;
                            _propertyFeatureRepository.Add(feature);
                        }

                        await _unitOfWork.CommitAllAsync();
                    }
                }
            }
            property.Bathrooms = viewModel.BathRooms;
            property.Bedrooms = viewModel.BedRooms;
            property.Garages = viewModel.Garages;
            _propertyRepository.Update(property);
            return await _unitOfWork.CommitAllAsync();
        }

        public async Task<bool> AddPropertyImages(int propertyId, string url, string publicId)
        {
            var image = new PropertyImage();
            image.PropertyId = propertyId;
            image.Url = url;
            image.PublicId = publicId;

            _propertyImageRepository.Add(image);
            return await _unitOfWork.CommitAllAsync();
        }
    }
}