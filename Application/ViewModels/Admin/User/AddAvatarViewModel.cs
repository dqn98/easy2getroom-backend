using Microsoft.AspNetCore.Http;

namespace BE.Application.ViewModels.Admin.User
{
    public class AddAvatarViewModel
    {
        public IFormFile File { get; set; }
        public string UserId { get; set; }
    }
}