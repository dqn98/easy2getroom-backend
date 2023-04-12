using BE.Infrastructure.ShareKernel;
using System.Collections.Generic;

namespace BE.Data.Entities
{
    public class AnnouncementType : DomainEntity<int>
    {
        public string Name { get; set; }
        public string Icon { get; set; }

        public virtual ICollection<Announcement> Announcements { get; set; }
    }
}