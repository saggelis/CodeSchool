using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlipWeen.Common.Entities
{
    public interface IEntityBase
    {
        int Id { get; set; }
    }


    public class EntityBase
    {
        private string _globalId;
        public string GlobalId
        {
            get
            {
                return _globalId;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    _globalId = Guid.NewGuid().ToString();
            }
        }

        [Timestamp, ConcurrencyCheck]
        public virtual byte[] RowVersion { get; set; }
    }
}
