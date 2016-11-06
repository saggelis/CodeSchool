using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlipWeen.Common.Entities
{
    public class Project : EntityBase, IEntityBase
    {
        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }

        [Required, MaxLength(100)]
        public virtual string Name { get; set; }

        [MaxLength(4000)]
        public virtual string Description { get; set; }

        [Required]
        public virtual DateTime CreationDate { get; set; }

        [Required]
        public virtual double TargetAmount { get; set; }

        [Required]
        public virtual int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser Application { get; set; }
     
    }
}
