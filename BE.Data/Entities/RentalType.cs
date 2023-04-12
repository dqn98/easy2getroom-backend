using BE.Infrastructure.ShareKernel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE.Data.Entities
{
    [Table("RentalTypes")]
    public class RentalType : DomainEntity<int>
    {
        public RentalType()
        {
            Properties = new HashSet<Property>();
        }

        public RentalType(string name)
        {
            Name = name;
        }

        public RentalType(int id, string name)
        {
            Id = id;
            Name = name;
        }

        [StringLength(255)]
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Property> Properties { get; set; }
    }
}