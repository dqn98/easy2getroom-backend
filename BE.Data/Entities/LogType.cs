using BE.Infrastructure.ShareKernel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE.Data.Entities
{
    [Table("LogTypes")]
    public class LogType : DomainEntity<int>
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public virtual ICollection<Log> Logs { get; set; }
    }
}