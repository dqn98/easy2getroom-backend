using BE.Data.Interfaces;
using BE.Infrastructure.ShareKernel;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE.Data.Entities
{
    [Table("Logs")]
    public class Log : DomainEntity<int>, IDateTracking
    {
        public string Content { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public int TypeId { get; set; }

        [ForeignKey("TypeId")]
        public virtual LogType Type { get; set; }
    }
}