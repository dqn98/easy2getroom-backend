using System;

namespace BE.Application.ViewModels.Client
{
    public class DeletePropertyViewModel
    {
        public int PropertyId { get; set; }
        public Guid UserId { get; set; }
    }
}