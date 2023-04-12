using BE.Infrastructure.ShareKernel;
using System.Collections.Generic;

namespace BE.Data.Entities
{
    public class Feature : DomainEntity<int>
    {
        public string Name { get; set; }

        public virtual ICollection<PropertyFeature> PropertyFeatures { get; set; }
    }
}