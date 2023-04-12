using System;

namespace BE.Application.ViewModels.Admin.Comment
{
    public class GetChildCommentsViewModel
    {
        public int CommentId { get; set; }
        public Guid UserId { get; set; }
    }
}