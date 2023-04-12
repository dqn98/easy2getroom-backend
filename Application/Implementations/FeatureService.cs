using BE.Application.Interfaces;
using BE.Application.ViewModels.Admin.Feature;
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
    public class FeatureService : IFeatureService
    {
        private IFeatureRepository _featureRepository;
        private IUnitOfWork _unitOfWork;

        public FeatureService(IFeatureRepository featureRepository, IUnitOfWork unitOfWork)
        {
            _featureRepository = featureRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Add(FeatureViewModel viewModel)
        {
            Feature feature = new Feature();
            feature.Name = viewModel.Name;

            _featureRepository.Add(feature);
            return await _unitOfWork.CommitAllAsync();
        }

        public async Task<bool> Delete(int id)
        {
            _featureRepository.Remove(id);
            return await _unitOfWork.CommitAllAsync();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Task<Feature> GetFeature(int id)
        {
            return _featureRepository.FindByIdAsync(id);
        }

        public Task<List<Feature>> GetFeatures(FeatureViewModel viewModel)
        {
            return _featureRepository.FindAll().
                Where(f => (viewModel.Name == "") ? true : f.Name.Contains(viewModel.Name)).ToListAsync();
        }

        public Task<List<Feature>> GetFeatures()
        {
            return _featureRepository.FindAll().ToListAsync();
        }

        public async Task<bool> Update(Feature feature)
        {
            _featureRepository.Update(feature);
            return await _unitOfWork.CommitAllAsync();
        }
    }
}