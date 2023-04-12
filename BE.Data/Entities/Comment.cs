using BE.Data.Interfaces;
using BE.Infrastructure.ShareKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE.Data.Entities
{
    public class Comment : DomainEntity<int>, IDateTracking
    {
        public Guid UserId { get; set; }

        public int PropertyId { get; set; }

        public string Content { get; set; }

        public int? ParentId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("PropertyId")]
        public virtual Property Property { get; set; }

        [ForeignKey("ParentId")]
        public virtual Comment Parent { get; set; }

        public virtual ICollection<Comment> Childs { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}