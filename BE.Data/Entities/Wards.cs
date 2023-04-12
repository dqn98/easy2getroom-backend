using BE.Infrastructure.ShareKernel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE.Data.Entities
{
    [Table("Wards")]
    public class Wards : DomainEntity<int>
    {
        public int DistrictId { get; set; }

        [StringLength(255)]
        [Required]
        public string Name { get; set; }

        [ForeignKey("DistrictId")]
        public virtual District District { get; set; }

        public virtual ICollection<Property> Properties { get; set; }
    }
}