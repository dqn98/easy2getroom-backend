using System;

namespace BE.Application.ViewModels.Admin.Comment
{
    public class LikeViewModel
    {
        public Guid UserId { get; set; }
        public int CommentId { get; set; }
    }
}