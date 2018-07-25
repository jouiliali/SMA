using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMA.Entities.Models
{
    public partial class Attendance : BaseEntity
    {
      
        public bool IsPresent { get; set; }
        public DateTime CreatedDate { get; set; }

        [ForeignKey("StudentId")]
        public virtual Student Student { get; set; }
        public Int64 StudentId { get; set; }

        [ForeignKey("ScheduleId")]
        public virtual Schedule Schedule { get; set; }
        public Int64 ScheduleId { get; set; }

        [ForeignKey("SchoolYearId")]
        public virtual SchoolYear SchoolYear { get; set; }
        public Int64 SchoolYearId { get; set; }
    }
}
