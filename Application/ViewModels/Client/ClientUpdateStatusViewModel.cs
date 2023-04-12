using BE.Data.Enums;
using System;

namespace BE.Application.ViewModels.Client
{
    public class ClientUpdateStatusViewModel
    {
        public int Id { get; set; }
        public Status Status { get; set; }
        public Guid UserId { get; set; }
    }
}