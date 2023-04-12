using Microsoft.AspNetCore.Http;

namespace BE.ViewModels.Admin.PropertyImage
{
    public class AddPropertyImageViewModel
    {
        public string Url { get; set; }
        public IFormFile File { get; set; }
        public string PublicId { get; set; }
        public int PropertyId { get; set; }
    }
}