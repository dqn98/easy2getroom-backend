using BE.Application.Interfaces;
using BE.Application.ViewModels.Admin.Comment;
using BE.Application.ViewModels.Admin.User;
using BE.Application.ViewModels.Client;
using BE.Application.ViewModels.Shared;
using BE.Data.Entities;
using BE.Data.IRepositories;
using BE.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE.Application.Implementations
{
    public class CommentService : ICommentService
    {
        private ICommentRepository _commentRepository;
        private IUnitOfWork _unitOfWork;
        private UserManager<User> _userManager;

        public CommentService(ICommentRepository commentRepository, IUnitOfWork unitOfWork,
            UserManager<User> userManager)
        {
            _commentRepository = commentRepository;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<Comment> AddComment(AddCommentViewModel viewModel)
        {
            var comment = new Comment();
            comment.UserId = viewModel.UserId;
            comment.PropertyId = viewModel.PropertyId;
            comment.Content = viewModel.Content;

            if (viewModel.ParentId != null)
            {
                comment.ParentId = viewModel.ParentId;
            }

            _commentRepository.Add(comment);
            var result = await _unitOfWork.CommitAllAsync();
            if (result)
            {
                return comment;
            }
            return null;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<List<CommentResultViewModel>> GetChildComments(GetChildCommentsViewModel viewModel)
        {
            var comments = _commentRepository.FindAll(c => c.User).Where(c => c.ParentId == viewModel.CommentId).ToList();
            var result = new List<CommentResultViewModel>();
            foreach (Comment c in comments)
            {
                var vm = new CommentResultViewModel();
                vm.User = new UserResultViewModel();

                vm.Id = c.Id;
                vm.UserId = c.UserId;
                vm.Content = c.Content;
                vm.ParentId = c.ParentId;
                vm.PropertyId = c.PropertyId;
                vm.User.Id = c.User.Id;

                var user = await _userManager.FindByIdAsync(c.User.Id.ToString());
                var roles = await _userManager.GetRolesAsync(user);
                var roleName = roles.FirstOrDefault();

                vm.User.RoleName = roleName;
                vm.User.FullName = c.User.FullName;
                vm.User.Avatar = c.User.Avatar;

                vm.DateCreated = c.DateCreated;
                vm.DateModified = c.DateModified;

                result.Add(vm);
            }

            return result;
        }

        public async Task<List<CommentResultViewModel>> GetComments(GetCommentViewModel viewModel)
        {
            var comments = _commentRepository.FindAll(c => c.User, c => c.Childs).Where(c => c.PropertyId == viewModel.PropertyId && c.ParentId == null)
                .OrderBy(c => c.DateCreated).ToList();
            var result = new List<CommentResultViewModel>();
            foreach (Comment c in comments)
            {
                var vm = new CommentResultViewModel();
                vm.User = new UserResultViewModel();

                vm.Id = c.Id;
                vm.UserId = c.UserId;
                vm.Content = c.Content;
                vm.ParentId = c.ParentId;
                vm.User.Id = c.User.Id;
                vm.PropertyId = c.PropertyId;

                var user = await _userManager.FindByIdAsync(c.User.Id.ToString());
                var roles = await _userManager.GetRolesAsync(user);
                var roleName = roles.FirstOrDefault();

                vm.User.RoleName = roleName;
                vm.User.FullName = c.User.FullName;
                vm.User.Avatar = c.User.Avatar;

                vm.DateCreated = c.DateCreated;
                vm.DateModified = c.DateModified;

                if (c.Childs.Count() > 0)
                {
                    vm.ChildComments = new List<CommentResultViewModel>();
                    var getChildVM = new GetChildCommentsViewModel()
                    {
                        UserId = viewModel.UserId,
                        CommentId = c.Id
                    };
                    var childs = await GetChildComments(getChildVM);
                    vm.ChildComments = childs;
                }

                result.Add(vm);
            }

            return result;
        }

        public async Task<List<CommentViewModel>> GetComments(int propertyId)
        {
            var comments = await _commentRepository.FindAll(c => c.User).Where(c => c.PropertyId == propertyId && c.ParentId == null).ToListAsync();
            var commentsVM = new List<CommentViewModel>();

            foreach (Comment comment in comments)
            {
                var commentVM = new CommentViewModel();
                commentVM = await MappingClientDataAsync(comment);
                var childs = await _commentRepository.FindAll(c => c.User).Where(c => c.ParentId == comment.Id).ToListAsync();
                var childsVM = new List<CommentViewModel>();
                foreach(Comment child in childs)
                {
                    var childVM = new CommentViewModel();
                    childVM = await MappingClientDataAsync(child);
                    childsVM.Add(childVM);
                }
                commentVM.Childs = new List<CommentViewModel>();
                commentVM.Childs = childsVM;
                commentsVM.Add(commentVM);      
            }

            return commentsVM;
        }

        public async Task<CommentViewModel> MappingClientDataAsync(Comment comment)
        {
            var cm = new CommentViewModel();
            cm.Id = comment.Id;
            cm.Author = new ClientUserViewModel();
            cm.Author = await MappingUserAsync(comment.User);
            cm.ParentId = comment.ParentId;
            cm.Content = comment.Content;
            cm.Date = comment.DateCreated;
            return cm;
        }

        public async Task<ClientUserViewModel> MappingUserAsync(User user)
        {
            var role = await _userManager.GetRolesAsync(user);

            var vm = new ClientUserViewModel();
            vm.Id = user.Id;
            vm.Username = user.UserName;
            vm.FullName = user.FullName;
            vm.Desc = "Address: " + user.Address + ", Date registed: " + user.DateCreated.ToString();
            vm.Organization = "Easy2GetRoom";
            vm.Email = user.Email;
            vm.Phone = user.PhoneNumber;

            vm.Social = new Social();
            vm.Social.Facebook = user.FacebookUrl;
            vm.Social.Twitter = user.TwitterUrl;
            vm.Social.Website = user.WebsiteUrl;
            vm.Social.Instagram = "";
            vm.Social.Linkedin = "";
            vm.RoleName = role.FirstOrDefault();
            vm.Image = user.Avatar;

            return vm;
        }
    }
}