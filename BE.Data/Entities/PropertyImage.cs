using BE.Infrastructure.ShareKernel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE.Data.Entities
{
    [Table("PropertyImages")]
    public class PropertyImage : DomainEntity<int>
    {
        public PropertyImage()
        {
        }

        public PropertyImage(string url, int propertyId)
        {
            Url = url;
            PropertyId = propertyId;
        }

        public PropertyImage(int id, string url, int propertyId)
        {
            Id = id;
            Url = url;
            PropertyId = propertyId;
        }

        [StringLength(1000)]
        [Required]
        public string Url { get; set; }

        public string PublicId { get; set; }

        public int PropertyId { get; set; }
        public virtual Property Property { get; set; }
    }
}