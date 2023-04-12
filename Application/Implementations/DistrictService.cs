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
    public class DistrictService : IDistrictService
    {
        private IDistrictRepository _districtRepository;
        private IUnitOfWork _unitOfWork;

        public DistrictService(IDistrictRepository districtRepository, IUnitOfWork unitOfWork)
        {
            _districtRepository = districtRepository;
            _unitOfWork = unitOfWork;
        }

        public void Delete(int id)
        {
            _districtRepository.Remove(id);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Task<List<District>> GetAll()
        {
            return _districtRepository.FindAll().ToListAsync();
        }

        public District GetById(int id)
        {
            return _districtRepository.FindById(id);
        }

        public Task<List<District>> GetDistrictbyCityId(int id)
        {
            return _districtRepository.FindAll(x => x.CityId == id).OrderBy(x => x.Name).ToListAsync();
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(District district)
        {
            _districtRepository.Update(district);
        }

        public void UpdateStatus(int id, Status status)
        {
            throw new NotImplementedException();
        }
    }
}