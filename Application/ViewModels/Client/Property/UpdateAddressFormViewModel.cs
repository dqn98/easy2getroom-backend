using System;
using System.Collections.Generic;
using System.Text;

namespace BE.Application.ViewModels.Client.Property
{
    public class UpdateAddressFormViewModel
    {
        public string Address { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public int WardsId { get; set; }
    }
}
