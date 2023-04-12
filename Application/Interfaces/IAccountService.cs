using BE.Application.ViewModels.Client;
using BE.Application.ViewModels.Client.Property;
using BE.Application.ViewModels.Client.Rating;
using BE.Application.ViewModels.Client.User;
using BE.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE.Application.Interfaces
{
    public interface IAccountService : IDisposable
    {
        Task<List<PropertyVM>> GetUserProperties(Guid userId);

        Task<List<PropertyVM>> GetFavorites(Guid userId);

        Task<bool> Favorite(FavoriteViewModel viewModel);

        Task<bool> Unfavorite(FavoriteViewModel viewModel);

        Task<bool> Rating(RatingViewModel viewModel);

        Task<int> CheckRating(CheckRatingViewModel viewModel);

        PropertyVM MappingData(Property p);

        Task<List<PropertyVM>> GetMyProperties(GetUserPropertiesViewModel viewModel);

        Task<int> SubmitProperty(SubmitPropertyViewModel viewModel);

        Task<bool> AddPropertyFeatures(AddPropertyFeaturesViewModel viewModel);

        Task<bool> UpdateStatus(ClientUpdateStatusViewModel viewModel);

        Task<bool> DeleteProperty(DeletePropertyViewModel viewModel);

        Task<ProfileViewModel> GetProfile(string username);

        Task<bool> UpdateProfile(UpdateProfileViewModel viewModel);

        Task<bool> UpdateAvatar(string id, string avatar, string avatarPublicId);

        Task<bool> UpdateBasicForm(UpdateBasicFormViewModel viewModel, int propertyId);

        Task<bool> UpdateAddressForm(UpdateAddressFormViewModel viewModel, int propertyId);

        Task<bool> UpdateAdditionalForm(UpdateAdditionalFormViewModel viewModel, int propertyId);

        Task<bool> AddPropertyImages(int propertyId, string url, string publicId);
    }
}