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
    public class RentalTypeService : IRentalTypeService
    {
        private IRentalTypeRepository _rentalTypeRepository;
        private IUnitOfWork _unitOfWork;

        public RentalTypeService(IRentalTypeRepository rentalTypeRepository, IUnitOfWork unitOfWork)
        {
            _rentalTypeRepository = rentalTypeRepository;
            _unitOfWork = unitOfWork;
        }

        public void Delete(int id)
        {
            _rentalTypeRepository.Remove(id);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Task<List<RentalType>> GetAll()
        {
            return _rentalTypeRepository.FindAll().ToListAsync();
        }

        public RentalType GetById(int id)
        {
            return _rentalTypeRepository.FindById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(RentalType rentalType)
        {
            _rentalTypeRepository.Update(rentalType);
        }

        public void UpdateStatus(int id, Status status)
        {
            throw new NotImplementedException();
        }
    }
}