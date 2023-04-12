using BE.Infrastructure.ShareKernel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE.Data.Entities
{
    [Table("PropertyCategories")]
    public class PropertyCategory : DomainEntity<int>
    {
        public PropertyCategory()
        {
            Properties = new HashSet<Property>();
        }

        public PropertyCategory(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public PropertyCategory(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        [StringLength(255)]
        [Required]
        public string Name { get; set; }

        [StringLength(1000)]
        [Required]
        public string Description { get; set; }

        public virtual ICollection<Property> Properties { get; set; }
    }
}