using System.ComponentModel.DataAnnotations.Schema;

namespace BE.Data.Entities
{
    public class PropertyFeature
    {
        public int PropertyId { get; set; }
        public int FeatureId { get; set; }

        [ForeignKey("PropertyId")]
        public virtual Property Property { get; set; }

        [ForeignKey("FeatureId")]
        public virtual Feature Feature { get; set; }
    }
}