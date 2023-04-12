using BE.Infrastructure.ShareKernel;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE.Data.Entities
{
    [Table("Messages")]
    public class Message : DomainEntity<int>
    {
        public string Connectionid { get; set; }
        public Guid SenderId { get; set; }
        public virtual User Sender { get; set; }
        public Guid RecipientId { get; set; }
        public virtual User Recipient { get; set; }
        public string Content { get; set; }
        public string Status { get; set; }
        public bool IsRead { get; set; }
        public bool? IsGroup { get; set; }
        public bool? IsMultiple { get; set; }
        public bool? IsPrivate { get; set; }
        public DateTime? DateRead { get; set; }
        public DateTime MessageSent { get; set; }
    }
}