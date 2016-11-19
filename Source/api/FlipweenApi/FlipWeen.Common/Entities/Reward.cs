using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlipWeen.Common.Entities
{
    public class Reward : EntityBase, IEntityBase
    {
        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }

        [Required, MaxLength(200)]
        public virtual string Description { get; set; }

        [Required]
        public virtual int PackageId { get; set; }

        [ForeignKey("PackageId")]
        public virtual Package Package { get; set; }
    }
}
