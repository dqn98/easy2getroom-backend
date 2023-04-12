using System;

namespace BE.Application.ViewModels.Shared.Announcement
{
    public class AnnouncementViewModel
    {
        public int Id { get; set; }
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
        public string Content { get; set; }
        public string Type { get; set; }
        public string Icon { get; set; }
        public bool IsRead { get; set; }
        public DateTime Date { get; set; }
    }
}