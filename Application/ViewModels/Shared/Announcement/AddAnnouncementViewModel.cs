using System;
using System.Collections.Generic;
using System.Text;

namespace BE.Application.ViewModels.Shared.Announcement
{
    public class AddAnnouncementViewModel
    {
        public string Content { get; set; }
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
        public int AnnouncementTypeId { get; set; }
    }
}
