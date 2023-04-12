using BE.Data.Interfaces;
using BE.Infrastructure.ShareKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE.Data.Entities
{
    [Table("News")]
    public class News : DomainEntity<int>, IDateTracking
    {
        public News(string title, string content, Guid userId, DateTime dateCreated, DateTime dateModified)
        {
            Title = title;
            Content = content;
            UserId = userId;
            DateCreated = dateCreated;
            DateModified = dateModified;
        }

        public News(int id, string title, string content, Guid userId, DateTime dateCreated, DateTime dateModified)
        {
            Id = id;
            Title = title;
            Content = content;
            UserId = userId;
            DateCreated = dateCreated;
            DateModified = dateModified;
        }

        public string Title { get; set; }

        [StringLength(10000)]
        [Required]
        public string Content { get; set; }

        public Guid UserId { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public virtual ICollection<NewsImage> NewsImages { get; set; }
    }
}