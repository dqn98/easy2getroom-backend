namespace BE.Application.ViewModels.Shared
{
    public class ChangePasswordViewModel
    {
        public string UserId { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}