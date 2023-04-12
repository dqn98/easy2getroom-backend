using BE.Infrastructure.ShareKernel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE.Data.Entities
{
    [Table("Cities")]
    public class City : DomainEntity<int>
    {
        [Required]
        public string Name { get; set; }

        public virtual ICollection<District> Districts { get; set; }
    }
}