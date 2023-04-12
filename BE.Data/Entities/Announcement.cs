using BE.Data.Interfaces;
using BE.Infrastructure.ShareKernel;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE.Data.Entities
{
    [Table("Announcements")]
    public class Announcement : DomainEntity<int>, IDateTracking
    { 
        public string Content { get; set; }

        [DefaultValue(false)]
        public bool IsRead { get; set; }

        public Guid SenderId { get; set; }

        [ForeignKey("SenderId")]
        public virtual User Sender { get; set; }

        public Guid ReceiverId { get; set; }

        [ForeignKey("ReceiverId")]
        public virtual User Receiver { get; set; }

        public int AnnouncementTypeId { get; set; }

        [ForeignKey("AnnouncementTypeId")]
        public virtual AnnouncementType AnnouncementType { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}