using System;
using System.Collections.Generic;

namespace BE.Application.ViewModels.Admin.Property
{
    public class GetPropertiesViewModel
    {
        public HashSet<int> Status { get; set; }
        public HashSet<int> PropertyCategoryIds { get; set; }
        public HashSet<int> RentalTypeIds { get; set; }
        public string Keyword { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
    }
}