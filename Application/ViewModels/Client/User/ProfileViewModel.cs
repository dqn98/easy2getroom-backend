using Microsoft.AspNetCore.Http;
using System;

namespace BE.Application.ViewModels.Client.User
{
    public class ProfileViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public IFormFile Image { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime? Birthday { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Website { get; set; }

        public string Avatar { get; set; }
        public string AvatarPublicId { get; set; }
    }
}