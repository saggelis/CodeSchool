using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlipWeen.MVC.Models
{
    public class PackageViewModel
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public double Amount { get; set; }
        
    }
}
