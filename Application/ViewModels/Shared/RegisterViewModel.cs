using System;

namespace BE.Application.ViewModels.Shared
{
    public class RegisterViewModel
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public DateTime BirthDay { get; set; }
        public string Avatar { get; set; }
        public string AvatarPublicId { get; set; }
    }
}