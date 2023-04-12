using System;
using System.Collections.Generic;
using System.Text;

namespace BE.Application.ViewModels.Client
{
    public class AddPropertyFeaturesViewModel
    {
        public int PropertyId { get; set; }
        public List<int> Features { get; set; }
    }
}
