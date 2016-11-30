using FlipWeen.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlipWeen.Common.Entities
{
    public class ProjectViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime EndDate { get; set; }

        public double TargetAmount { get; set; }

        public int UserId { get; set; }

        public int CategoryId { get; set; }

        public string Image { get; set; }

        public string Video { get; set; }

        public int ProgressPercent { get; set; }

        public int BackersNo { get; set; }

        public int DaysLeft { get; set; }

        public double CurrentAmount { get; set; }

        public UserInfoViewModel User { get; set; }

    }
}
