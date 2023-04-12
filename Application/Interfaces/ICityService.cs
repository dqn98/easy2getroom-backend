using BE.Data.Entities;
using BE.Data.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE.Application.Interfaces
{
    public interface ICityService : IDisposable
    {
        Task<List<City>> GetAll();

        City GetById(int id);

        void UpdateStatus(int id, Status status);

        void Delete(int id);

        void Update(City city);

        void Save();
    }
}