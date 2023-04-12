using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE.Data.Entities
{
    [Table("Favorites")]
    public class Favorite
    {
        public Guid UserId { get; set; }
        public int PropertyId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("PropertyId")]
        public virtual Property Property { get; set; }
    }
}