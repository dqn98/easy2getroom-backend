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
    public class WardsService : IWardsService
    {
        private IWardsRepository _wardsRepository;
        private IUnitOfWork _unitOfWork;

        public WardsService(IWardsRepository wardsRepository, IUnitOfWork unitOfWork)
        {
            _wardsRepository = wardsRepository;
            _unitOfWork = unitOfWork;
        }

        public void Delete(int id)
        {
            _wardsRepository.Remove(id);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Task<List<Wards>> GetAll()
        {
            return _wardsRepository.FindAll().ToListAsync();
        }

        public Wards GetById(int id)
        {
            return _wardsRepository.FindById(id);
        }

        public Task<List<Wards>> GetWardsByDistrictId(int id)
        {
            return _wardsRepository.FindAll(x => x.DistrictId == id).OrderBy(x => x.Name).ToListAsync();
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Wards wards)
        {
            _wardsRepository.Update(wards);
        }

        public void UpdateStatus(int id, Status status)
        {
            throw new NotImplementedException();
        }
    }
}