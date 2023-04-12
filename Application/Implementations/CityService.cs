using BE.Application.Interfaces;
using BE.Data.Entities;
using BE.Data.Enums;
using BE.Data.IRepositories;
using BE.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE.Application.Implementations
{
    public class CityService : ICityService
    {
        private ICityRepository _cityRepository;
        private IUnitOfWork _unitOfWork;

        public CityService()
        {
        }

        public CityService(ICityRepository cityRepository, IUnitOfWork unitOfWork)
        {
            _cityRepository = cityRepository;
            _unitOfWork = unitOfWork;
        }

        public void Delete(int id)
        {
            _cityRepository.Remove(id);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Task<List<City>> GetAll()
        {
            return _cityRepository.FindAll().OrderBy(x => x.Name).ToListAsync();
        }

        public City GetById(int id)
        {
            City city = _cityRepository.FindById(id);
            return city;
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(City city)
        {
            _cityRepository.Update(city);
        }

        public void UpdateStatus(int id, Status status)
        {
            throw new NotImplementedException();
        }
    }
}