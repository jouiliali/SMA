using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace SMA.Entities.Models
{
    public partial class Exam : BaseEntity
    {
        public DateTime At { get; set; }
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }

        [ForeignKey("ClassId")]
        public virtual Class Class { get; set; }
        public Int64 ClassId { get; set; }

        [ForeignKey("SubjectId")]
        public virtual Subject Subject { get; set; }
        public Int64 SubjectId { get; set; }

        [ForeignKey("ClassRoomId")]
        public virtual ClassRoom ClassRoom { get; set; }
        public Int64 ClassRoomId { get; set; }

        [ForeignKey("SchoolYearId")]
        public virtual SchoolYear SchoolYear { get; set; }
        public Int64 SchoolYearId { get; set; }

        [ForeignKey("AssessmentId")]
        public virtual Assessment Assessment { get; set; }
        public Int64 AssessmentId { get; set; }

        public virtual List<Mark> Marks { get; set; }

        public Exam()
        {
            Marks = new List<Mark>();
        }

    }
}
