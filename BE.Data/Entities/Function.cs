using BE.Data.Interfaces;
using BE.Infrastructure.ShareKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE.Data.Entities
{
    [Table("Functions")]
    public class Function : DomainEntity<int>, ISortable
    {
        public Function()
        {
        }

        public Function(string name, string uRL, string iconCss, int sortOrder)
        {
            Name = name;
            URL = uRL;
            IconCss = iconCss;
            SortOrder = sortOrder;
        }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [Required]
        [StringLength(250)]
        public string URL { get; set; }

        public string IconCss { get; set; }
        public int SortOrder { get; set; }
    }
}