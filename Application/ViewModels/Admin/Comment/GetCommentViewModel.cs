using System;

namespace BE.Application.ViewModels.Admin.Comment
{
    public class GetCommentViewModel
    {
        public int PropertyId { get; set; }
        public Guid UserId { get; set; }
    }
}