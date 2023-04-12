using System;

namespace BE.ViewModels.Admin.User
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }

        public string UserName { get; set; }
        public string Email { get; set; }

        public DateTime? BirthDay { get; set; }

        public string Avatar { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }
    }
}