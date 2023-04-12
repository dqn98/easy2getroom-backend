using BE.Application.Interfaces;
using BE.Application.ViewModels.Admin.Property;
using BE.Application.ViewModels.UpdatePropertyAdmin.Property;
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
    public class PropertyService : IPropertyService
    {
        private IPropertyRepository _propertyRepository;
        private IPropertyImageRepository _propertyImageRepository;
        private IRatingRepository _ratingRepository;
        private IFavoriteRepository _favoriteRepository;
        private ICommentRepository _commentRepository;
        private IPropertyFeatureRepository _propertyFeatureRepository;
        private UserManager<User> _userManager;
        private IUnitOfWork _unitOfWork;

        public PropertyService(IPropertyRepository propertyRepository,
            IPropertyImageRepository propertyImageRepository, IRatingRepository ratingRepository, IFavoriteRepository favoriteRepository, ICommentRepository commentRepository, IPropertyFeatureRepository propertyFeatureRepository,
            UserManager<User> userManager, IUnitOfWork unitOfWork)
        {
            _propertyRepository = propertyRepository;
            _propertyImageRepository = propertyImageRepository;
            _ratingRepository = ratingRepository;
            _favoriteRepository = favoriteRepository;
            _commentRepository = commentRepository;
            _propertyFeatureRepository = propertyFeatureRepository;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Delete(int id)
        {
            var property = await _propertyRepository.FindAll(p => p.PropertyFeatures, p => p.Ratings, p => p.Favorites, p => p.Comments, p => p.PropertyImages)
                .Where(p => p.Id == id)
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

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Task<List<Property>> GetAll()
        {
            return _propertyRepository.FindAll().ToListAsync();
        }

        public Task<List<Property>> GetProperties(GetPropertiesViewModel viewModel)
        {
            var query = _propertyRepository.FindAll(p => p.Wards.District.City)
                .Where(p => viewModel.Status.Count == 0 ? true : viewModel.Status.Contains((int)p.Status)
                && (viewModel.PropertyCategoryIds.Count == 0 ? true : viewModel.PropertyCategoryIds.Contains(p.PropertyCategoryId))
                && (viewModel.RentalTypeIds.Count == 0 ? true : viewModel.RentalTypeIds.Contains(p.RentalTypeId))
                && (viewModel.Keyword == "" ? true : p.Title.Contains(viewModel.Keyword) || p.Address.Contains(viewModel.Keyword)
                   || p.Wards.District.Name.Contains(viewModel.Keyword) || p.Wards.District.City.Name.Contains(viewModel.Keyword))
                && (viewModel.DateStart == null ? true : (p.DateCreated >= viewModel.DateStart))
                && (viewModel.DateEnd == null ? true : (p.DateCreated <= viewModel.DateEnd))).OrderByDescending(p => p.DateCreated);
            return query.ToListAsync();
        }

        public Property GetById(int id)
        {
            return _propertyRepository.FindById(id, p => p.Wards, p => p.RentalType, p => p.User, p => p.PropertyCategory);
        }

        public Task<Property> GetByIdAsync(int id)
        {
            var data = _propertyRepository.FindAll(p => p.Wards, p => p.Wards.District,
                p => p.Wards.District.City, p => p.RentalType, p => p.User, p => p.PropertyCategory, p => p.PropertyImages).Include(p => p.PropertyFeatures).ThenInclude(pf => pf.Feature);
            return data.FirstAsync(x => x.Id == id);
        }

        public void Update(Property property)
        {
            _propertyRepository.Update(property);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public Task<int> UpdateStatus(UpdateStatusViewModel viewModel)
        {
            var property = _propertyRepository.FindById(viewModel.Id);
            property.Status = viewModel.Status;
            _propertyRepository.Update(property);
            return _unitOfWork.CommitAsync();
        }

        public void AddImages(string[] imagesUrls, int propertyId)
        {
            _propertyImageRepository.RemoveMultiple(_propertyImageRepository.FindAll(x => x.PropertyId == propertyId).ToList());
            foreach (var url in imagesUrls)
            {
                _propertyImageRepository.Add(new PropertyImage()
                {
                    Url = url,
                    PropertyId = propertyId
                });
            }
        }

        public Property Add(Property property)
        {
            _propertyRepository.Add(property);
            _unitOfWork.Commit();
            return property;
        }

        public User GetPropertyOwnerByPropertyId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Property> GetLastestProperties()
        {
            var query = _propertyRepository
                .FindAll()
                .OrderByDescending(p => p.DateCreated)
                .Where(x => x.Status == Status.Active)
                .Take(CommonConstants.AmountLastestProperty)
                .ToList();
            return query;
        }

        public List<Property> GetRelatedProperties(int id, int top)
        {
            var property = _propertyRepository.FindById(id);
            return _propertyRepository.FindAll(x => x.Status == Status.Active
                && x.Id != property.Id && x.PropertyCategoryId == property.PropertyCategoryId)
                .Take(top)
                .ToList();
        }

        public async Task<List<Property>> GetPropertiesUser(GetPropertiesUserViewModel viewModel)
        {
            var user = await _userManager.FindByNameAsync(viewModel.Username);
            if (user != null)
            {
                var query = _propertyRepository.FindAll(p => p.Wards.District.City).Where(p => p.UserId == user.Id
                  && viewModel.Status.Count == 0 ? true : viewModel.Status.Contains((int)p.Status)
                  && (viewModel.PropertyCategoryIds.Count == 0 ? true : viewModel.PropertyCategoryIds.Contains(p.PropertyCategoryId))
                  && (viewModel.RentalTypeIds.Count == 0 ? true : viewModel.RentalTypeIds.Contains(p.RentalTypeId))
                  && (viewModel.Keyword == "" ? true : p.Title.Contains(viewModel.Keyword) || p.Address.Contains(viewModel.Keyword)
                     || p.Wards.District.Name.Contains(viewModel.Keyword) || p.Wards.District.City.Name.Contains(viewModel.Keyword))
                  && (viewModel.DateStart == null ? true : (p.DateCreated >= viewModel.DateStart))
                  && (viewModel.DateEnd == null ? true : (p.DateCreated <= viewModel.DateEnd)));
                return await query.ToListAsync();
            }

            return null;
        }
    }
}