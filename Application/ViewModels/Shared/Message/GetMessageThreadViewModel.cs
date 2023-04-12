using System;

namespace BE.Application.ViewModels.Shared.Message
{
    public class GetMessageThreadViewModel
    {
        public Guid UserId { get; set; }
        public Guid RecipientId { get; set; }
    }
}