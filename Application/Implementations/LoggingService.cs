using BE.Application.Interfaces;
using BE.Application.ViewModels.Admin.Logging;
using BE.Data.Entities;
using BE.Data.IRepositories;
using BE.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE.Application.Implementations
{
    public class LoggingService : ILoggingService
    {
        private ILoggingRepository _loggingRepository;
        private IUnitOfWork _unitOfWork;

        public LoggingService(ILoggingRepository loggingRepository, IUnitOfWork unitOfWork)
        {
            _loggingRepository = loggingRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<LogViewModel> AddLog(AddLogViewModel viewModel)
        {
            var log = new Log();
            log.Content = viewModel.Content;
            log.TypeId = viewModel.TypeId;

            _loggingRepository.Add(log);

            var isSaved = await _unitOfWork.CommitAllAsync();
            if (isSaved)
            {
                var newLog = await _loggingRepository.FindAll(l => l.Type).Where(l => l.Id == log.Id).FirstOrDefaultAsync();
                return MappingData(newLog);
            }
            return null;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<List<LogViewModel>> GetLogs(GetLogViewModel viewModel)
        {
            var logsFromDb = await _loggingRepository.FindAll(l => l.Type)
                .Where(l => (viewModel.Types.Count() == 0) ? true : viewModel.Types.Contains(l.TypeId)
                && viewModel.KeyWord == "" ? true : l.Content.Contains(viewModel.KeyWord)
                && viewModel.DateStart == null ? true : l.DateCreated >= viewModel.DateStart
                && viewModel.DateEnd == null ? true : l.DateCreated <= viewModel.DateEnd)
                .OrderByDescending(l => l.DateCreated)
                .ToListAsync();

            List<LogViewModel> logs = new List<LogViewModel>();

            foreach (Log log in logsFromDb)
            {
                var vm = MappingData(log);
                logs.Add(vm);
            }

            return logs;
        }

        public LogViewModel MappingData(Log log)
        {
            var logVM = new LogViewModel();
            logVM.Id = log.Id;
            logVM.Content = log.Content;
            logVM.DateCreated = log.DateCreated;
            logVM.DateModified = log.DateModified;
            logVM.TypeName = log.Type.Name;
            logVM.Icon = log.Type.Icon;

            return logVM;
        }
    }
}