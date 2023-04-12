using System;

namespace BE.Application.ViewModels.Shared.Message
{
    public class GetMessageViewModel
    {
        public Guid SenderId { get; set; }
        public Guid RecipientId { get; set; }
    }
}