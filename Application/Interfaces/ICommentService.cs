using BE.Application.ViewModels.Admin.Comment;
using BE.Application.ViewModels.Client;
using BE.Application.ViewModels.Shared;
using BE.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE.Application.Interfaces
{
    public interface ICommentService : IDisposable
    {
        Task<List<CommentResultViewModel>> GetComments(GetCommentViewModel viewModel);

        Task<List<CommentResultViewModel>> GetChildComments(GetChildCommentsViewModel viewModel);

        Task<Comment> AddComment(AddCommentViewModel viewModel);

        Task<List<CommentViewModel>> GetComments(int propertyId);

        Task<CommentViewModel> MappingClientDataAsync(Comment comment);

        Task<ClientUserViewModel> MappingUserAsync(User user);
    }
}