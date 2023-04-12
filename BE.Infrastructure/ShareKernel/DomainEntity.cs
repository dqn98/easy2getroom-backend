using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE.Infrastructure.ShareKernel
{
    public class DomainEntity<T>
    {
        [Key]
        public T Id { get; set; }

        /// <summary>
        /// True if domain Entity has an identity
        /// </summary>
        /// <returns></returns>

        public bool IsTransient()
        {
            return Id.Equals(default(T));
        }
    }
}