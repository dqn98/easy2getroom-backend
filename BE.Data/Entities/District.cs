using BE.Infrastructure.ShareKernel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE.Data.Entities
{
    [Table("Districts")]
    public class District : DomainEntity<int>
    {
        public int CityId { get; set; }

        [StringLength(255)]
        [Required]
        public string Name { get; set; }

        [ForeignKey("CityId")]
        public virtual City City { get; set; }

        public virtual ICollection<Wards> Wards { get; set; }
    }
}