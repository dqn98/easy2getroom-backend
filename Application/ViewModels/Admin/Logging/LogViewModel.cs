using System;

namespace BE.Application.ViewModels.Admin.Logging
{
    public class LogViewModel
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public string TypeName { get; set; }

        public string Icon { get; set; }
    }
}