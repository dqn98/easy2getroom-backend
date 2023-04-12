using BE.Application.Interfaces;
using BE.Data.Entities;
using BE.Data.IRepositories;
using BE.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE.Application.Implementations
{
    public class PropertyImageService : IPropertyImageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPropertyImageRepository _propertyImageRepository;

        public PropertyImageService(IUnitOfWork unitOfWork, IPropertyImageRepository propertyImageRepository)
        {
            _unitOfWork = unitOfWork;
            _propertyImageRepository = propertyImageRepository;
        }

        public void AddPropertyImage(PropertyImage propertyImage)
        {
            _propertyImageRepository.Add(propertyImage);
        }

        public void DeletePropertyImage(int id)
        {
            _propertyImageRepository.Remove(id);
        }

        public async Task<bool> DeletePropertyImageClient(int id)
        {
            _propertyImageRepository.Remove(id);
            return await _unitOfWork.CommitAllAsync();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Task<PropertyImage> GetPropertyImageById(int id)
        {
            return _propertyImageRepository.FindByIdAsync(id);
        }

        public List<PropertyImage> GetPropertyImageByPropertyId(int id)
        {
            var data = _propertyImageRepository.FindAll(x => x.PropertyId == id).ToList();
            return data;
        }

        public async Task<List<PropertyImage>> GetPropertyImages(int propertyId)
        {
            var data = await _propertyImageRepository.FindAll().Where(i => i.PropertyId == propertyId).ToListAsync();
            return data;
        }

        public Task<int> SaveAsync(PropertyImage propertyImage)
        {
            _propertyImageRepository.Add(propertyImage);
            return _unitOfWork.CommitAsync();
        }
    }
}