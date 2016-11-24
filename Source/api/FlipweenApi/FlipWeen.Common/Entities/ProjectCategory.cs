using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlipWeen.Common.Entities
{
    public class ProjectCategory : EntityBase, IEntityBase
    {
        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }

        [Required, MaxLength(100)]
        public virtual string Name { get; set; }

        [MaxLength(500)]
        public virtual string Image { get; set; }

        private ICollection<Project> _projects;

        public virtual ICollection<Project> Projects
        {
            get
            {
                return _projects ?? (_projects = new HashSet<Project>());
            }

            set
            {
                _projects = value;
            }
        }


    }
}
