using BE.Data.Entities;
using BE.Data.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE.Application.Interfaces
{
    public interface IRentalTypeService : IDisposable
    {
        Task<List<RentalType>> GetAll();

        RentalType GetById(int id);

        void UpdateStatus(int id, Status status);

        void Delete(int id);

        void Update(RentalType RentalType);

        void Save();
    }
}