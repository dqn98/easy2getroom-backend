using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE.Data.Entities
{
    public class Rating
    {
        public Guid UserId { get; set; }
        public int PropertyId { get; set; }
        public int Value { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("PropertyId")]
        public virtual Property Property { get; set; }
    }
}