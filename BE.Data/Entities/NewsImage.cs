using BE.Infrastructure.ShareKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE.Data.Entities
{
    [Table("NewsImages")]
    public class NewsImage : DomainEntity<int>
    {
        public NewsImage(int id, string url, int newsId)
        {
            Id = id;
            Url = url;
            NewsId = newsId;
        }

        [StringLength(255)]
        [Required]
        public string Url { get; set; }

        public int NewsId { get; set; }

        [ForeignKey("NewsId")]
        public virtual News News { get; set; }
    }
}