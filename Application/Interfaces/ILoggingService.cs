using BE.Application.ViewModels.Admin.Logging;
using BE.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE.Application.Interfaces
{
    public interface ILoggingService : IDisposable
    {
        Task<List<LogViewModel>> GetLogs(GetLogViewModel viewModel);

        Task<LogViewModel> AddLog(AddLogViewModel viewModel);

        LogViewModel MappingData(Log log);
    }
}