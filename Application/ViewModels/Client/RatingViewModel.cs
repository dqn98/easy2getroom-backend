using System;

namespace BE.Application.ViewModels.Client
{
    public class RatingViewModel
    {
        public Guid UserId { get; set; }
        public int PropertyId { get; set; }
        public int Value { get; set; }
    }
}