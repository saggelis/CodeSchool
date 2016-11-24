﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlipWeen.Common.Entities
{
    public class ProjectCategoryViewModel
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
      
        public string Image { get; set; }

        public ICollection<ProjectViewModel> Projects { get; set; }

    }
}
