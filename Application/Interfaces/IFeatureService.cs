using BE.Application.ViewModels.Admin.Feature;
using BE.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE.Application.Interfaces
{
    public interface IFeatureService : IDisposable
    {
        Task<List<Feature>> GetFeatures(FeatureViewModel viewModel);
        Task<List<Feature>> GetFeatures();
        Task<Feature> GetFeature(int id);
        Task<bool> Delete(int id);
        Task<bool> Update(Feature feature);
        Task<bool> Add(FeatureViewModel viewModel);
    }
}