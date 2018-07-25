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
    public partial class Schedule : BaseEntity
    {
      
        public string Day { get; set; }
        public string Note { get; set; }
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }

        [ForeignKey("ClassId")]
        public virtual Class Class { get; set; }
        public Int64 ClassId { get; set; }

        [ForeignKey("SubjectLevelId")]
        public virtual SubjectLevel SubjectLevel { get; set; }
        public Int64 SubjectLevelId { get; set; }

        [ForeignKey("ClassRoomId")]
        public virtual ClassRoom ClassRoom { get; set; }
        public Int64 ClassRoomId { get; set; }

        [ForeignKey("TeacherId")]
        public virtual Teacher Teacher { get; set; }
        public Int64 TeacherId { get; set; }

        [ForeignKey("SchoolYearId")]
        public virtual SchoolYear SchoolYear { get; set; }
        public Int64 SchoolYearId { get; set; }


        public virtual List<Attendance> Attendances { get; set; }

        public Schedule()
        {
            Attendances = new List<Attendance>();
        }
    }
}
