using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace SMA.Entities.Models
{
    public partial class Mark : BaseEntity
    {
        public int Score { get; set; }
        public string Note { get; set; }

        [ForeignKey("SubjectId")]
        public virtual Subject Subject { get; set; }
        public Int64 SubjectId { get; set; }

        [ForeignKey("StudentId")]
        public virtual Student Student { get; set; }
        public Int64 StudentId { get; set; }

        [ForeignKey("TeacherId")]
        public virtual Teacher Teacher { get; set; }
        public Int64 TeacherId { get; set; }

        [ForeignKey("SchoolYearId")]
        public virtual SchoolYear SchoolYear { get; set; }
        public Int64 SchoolYearId { get; set; }

        [ForeignKey("AssessmentId")]
        public virtual Assessment Assessment { get; set; }
        public Int64 AssessmentId { get; set; }

        [ForeignKey("ExamId")]
        public virtual Exam Exam { get; set; }
        public Int64? ExamId { get; set; }

    }
}
