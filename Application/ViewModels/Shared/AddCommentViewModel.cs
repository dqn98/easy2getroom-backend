using System;

namespace BE.Application.ViewModels.Shared
{
    public class AddCommentViewModel
    {
        public Guid UserId { get; set; }
        public int PropertyId { get; set; }
        public string Content { get; set; }
        public int? ParentId { get; set; }
    }
}