using System;

namespace BE.Application.ViewModels.Shared
{
    public class ExternalLoginViewModel
    {
        public string AuthToken { get; set; }
        public string Provider { get; set; }
        public string Email { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDay { get; set; }
        public string Address { get; set; }
    }
}