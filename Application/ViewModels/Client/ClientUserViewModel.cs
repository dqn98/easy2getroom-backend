using System;

namespace BE.Application.ViewModels.Client
{
    public class ClientUserViewModel
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string RoleName { get; set; }
        public string FullName { get; set; }
        public string Desc { get; set; }
        public string Organization { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Social Social { get; set; }
        public string Image { get; set; }
    }
}