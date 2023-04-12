using BE.Application.ViewModels.Admin.Logging;
using BE.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE.Application.Interfaces
{
    public interface ILogTypeService : IDisposable
    {
        Task<List<LogType>> GetLogTypes();

        Task<bool> AddLogType(AddLogTypeViewModel viewModel);
    }
}