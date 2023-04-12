using System;

namespace BE.Application.ViewModels.Admin.User
{
    public class UpdateUserViewModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDay { get; set; }
        public string RoleName { get; set; }
    }
}