using BE.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE.Application.Interfaces
{
    public interface IFunctionService : IDisposable
    {
        Task<List<Function>> GetAll();
    }
}