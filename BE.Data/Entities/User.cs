using BE.Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE.Data.Entities
{
    [Table("Users")]
    public class User : IdentityUser<Guid>, IDateTracking
    {
        public User()
        {
        }

        public User(string fullName, DateTime? birthDay, string avatar, string address, string avatarPublicId, string facebookUrl, string twitterUrl, string websiteUrl)
        {
            FullName = fullName;
            BirthDay = birthDay;
            Avatar = avatar;
            Address = address;
            AvatarPublicId = avatarPublicId;
            FacebookUrl = facebookUrl;
            TwitterUrl = twitterUrl;
            WebsiteUrl = websiteUrl;
        }

        [StringLength(255)]
        public string FullName { get; set; }

        public DateTime? BirthDay { get; set; }

        public string Avatar { get; set; }

        public string Address { get; set; }

        public string AvatarPublicId { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public string FacebookUrl { get; set; }
        public string TwitterUrl { get; set; }
        public string WebsiteUrl { get; set; }

        public virtual ICollection<Message> MessagesSent { get; set; }
        public virtual ICollection<Message> MessagesReceived { get; set; }
        public virtual ICollection<Favorite> Favorites { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<Announcement> SentAnnouncements { get; set; }
        public virtual ICollection<Announcement> ReceivedAnnouncements { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}