using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace SMA.Entities.Models
{
    public partial class Assessment : BaseEntity
    {
        public string Libel { get; set; }
        public int Coef { get; set; }

        [ForeignKey("TermId")]
        public virtual Term Term { get; set; }
        public Int64 TermId { get; set; }

        public virtual List<Exam> Exams { get; set; }
        public virtual List<Mark> Marks { get; set; }

        public Assessment()
        {
            Exams = new List<Exam>();
            Marks = new List<Mark>();
        }
    }
}
