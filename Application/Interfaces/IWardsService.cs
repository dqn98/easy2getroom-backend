using BE.Data.Entities;
using BE.Data.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE.Application.Interfaces
{
    public interface IWardsService : IDisposable
    {
        Task<List<Wards>> GetAll();

        Wards GetById(int id);

        void UpdateStatus(int id, Status status);

        void Delete(int id);

        void Update(Wards wards);

        void Save();

        Task<List<Wards>> GetWardsByDistrictId(int id);
    }
}