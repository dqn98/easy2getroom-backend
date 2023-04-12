using System;
using System.Collections.Generic;
using System.Text;

namespace BE.Application.ViewModels.Client.User
{
    public class UpdateProfileViewModel
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Website { get; set; }
        public DateTime Birthday { get; set; }
    }
}
