using BE.Application.ViewModels.Admin.User;
using System;
using System.Collections.Generic;

namespace BE.Application.ViewModels.Admin.Comment
{
    public class CommentResultViewModel
    {
        public int Id { get; set; }
        public Guid? UserId { get; set; }
        public string Content { get; set; }
        public int PropertyId { get; set; }
        public int? ParentId { get; set; }
        public List<CommentResultViewModel> ChildComments { get; set; }
        public UserResultViewModel User { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}