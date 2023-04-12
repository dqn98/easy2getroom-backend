using System;
using System.Collections.Generic;

namespace BE.Application.ViewModels.Client
{
    public class CommentViewModel
    {
        public int Id { get; set; }

        public ClientUserViewModel Author { get; set; }

        public int? ParentId { get; set; }

        public string Content { get; set; }

        public DateTime Date { get; set; }

        public List<CommentViewModel> Childs { get; set; }
    }
}