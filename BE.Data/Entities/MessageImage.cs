using BE.Data.Interfaces;
using BE.Infrastructure.ShareKernel;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE.Data.Entities
{
    [Table("MessageImages")]
    public class MessageImage : DomainEntity<int>, IDateTracking
    {
        public string Url { get; set; }

        public int MessageId { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        [ForeignKey("MessageId")]
        public virtual Message Message { get; set; }
    }
}