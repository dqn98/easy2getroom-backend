using System;

namespace BE.Application.ViewModels.Client.Rating
{
    public class CheckRatingViewModel
    {
        public Guid UserId { get; set; }
        public int PropertyId { get; set; }
    }
}