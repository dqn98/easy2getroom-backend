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
    public class PropertyCategoryService : IPropertyCategoryService
    {
        private IPropertyCategoryRepository _propertyCategoryRepository;
        private IUnitOfWork _unitOfWork;

        public PropertyCategoryService(IPropertyCategoryRepository propertyCategoryRepository, IUnitOfWork unitOfWork)
        {
            _propertyCategoryRepository = propertyCategoryRepository;
            _unitOfWork = unitOfWork;
        }

        public PropertyCategory Add(PropertyCategory propertyCategory)
        {
            _propertyCategoryRepository.Add(propertyCategory);

            return propertyCategory;
        }

        public void Delete(int id)
        {
            _propertyCategoryRepository.Remove(id);
        }

        public Task<List<PropertyCategory>> GetAll()
        {
            return _propertyCategoryRepository.FindAll().ToListAsync();
        }

        public Task<List<PropertyCategory>> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                return _propertyCategoryRepository.FindAll(x => x.Name.Contains(keyword) ||
                    x.Description.Contains(keyword))
                   .ToListAsync();
            }
            else
            {
                return _propertyCategoryRepository.FindAll().ToListAsync();
            }
        }

        public PropertyCategory GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(PropertyCategory propertyCategoryViewModal)
        {
            throw new NotImplementedException();
        }
    }
}