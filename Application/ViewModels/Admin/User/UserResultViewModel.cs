using System;

namespace BE.Application.ViewModels.Admin.User
{
    public class UserResultViewModel
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime? BirthDay { get; set; }
        public string Avatar { get; set; }
        public string FacebookUrl { get; set; }
        public string TwitterUrl { get; set; }
        public string WebsiteUrl { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public string RoleName { get; set; }
    }
}