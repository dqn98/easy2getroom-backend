using System;

namespace BE.Application.ViewModels.Admin.Logging
{
    public class GetLogViewModel
    {
        public string KeyWord { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public int[] Types { get; set; }
    }
}