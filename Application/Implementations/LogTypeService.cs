using BE.Application.Interfaces;
using BE.Application.ViewModels.Admin.Logging;
using BE.Data.Entities;
using BE.Data.IRepositories;
using BE.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE.Application.Implementations
{
    public class LogTypeService : ILogTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogTypeRepository _logTypeRepository;

        public LogTypeService(IUnitOfWork unitOfWork, ILogTypeRepository logTypeRepository)
        {
            _unitOfWork = unitOfWork;
            _logTypeRepository = logTypeRepository;
        }

        public async Task<bool> AddLogType(AddLogTypeViewModel viewModel)
        {
            var logType = new LogType();
            logType.Name = viewModel.Name;
            logType.Icon = viewModel.Icon;

            _logTypeRepository.Add(logType);

            return await _unitOfWork.CommitAllAsync();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<List<LogType>> GetLogTypes()
        {
            return await _logTypeRepository.FindAll().ToListAsync();
        } 
    }
}