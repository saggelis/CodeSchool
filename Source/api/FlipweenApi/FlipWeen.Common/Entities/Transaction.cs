using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlipWeen.Common.Entities
{
    public class Transaction : EntityBase, IEntityBase
    {
        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }
         
        [Required]
        public virtual DateTime CreationDate { get; set; }

        [Required]
        public virtual double Amount { get; set; }

        [Required]
        public virtual int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        [Required]
        public virtual int ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }
        
        public virtual int? PackageId { get; set; }

        [ForeignKey("PackageId")]
        public virtual Package Package { get; set; }
        
    }
}
