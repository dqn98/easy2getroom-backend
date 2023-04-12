using BE.Data.Entities;
using BE.Data.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE.Application.Interfaces
{
    public interface IDistrictService : IDisposable
    {
        Task<List<District>> GetAll();

        District GetById(int id);

        void UpdateStatus(int id, Status status);

        void Delete(int id);

        void Update(District district);

        void Save();

        Task<List<District>> GetDistrictbyCityId(int id);
    }
}