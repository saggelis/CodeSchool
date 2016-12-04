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
        public virtual DateTime EndDate { get; set; }

        [Required]
        public virtual double TargetAmount { get; set; }

        [Required]
        public virtual int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        [Required]
        public virtual int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual ProjectCategory Category { get; set; }
        
        [MaxLength(500)]
        public virtual string Image { get; set; }

        [MaxLength(500)]
        public virtual string Video { get; set; }

        [Required]
        public virtual bool IsPublished{ get; set; }
        
        private ICollection<Package> _packages;

        public virtual ICollection<Package> Packages
        {
            get
            {
                return _packages ?? (_packages = new HashSet<Package>());
            }

            set
            {
                _packages = value;
            }
        }

        private ICollection<Transaction> _transactions;

        public virtual ICollection<Transaction> Transactions
        {
            get
            {
                return _transactions ?? (_transactions = new HashSet<Transaction>());
            }

            set
            {
                _transactions = value;
            }
        }

    }
}
