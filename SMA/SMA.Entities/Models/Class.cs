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
    public partial class Class : BaseEntity
    {
      
        public string Libel { get; set; }

        [ForeignKey("LevelId")]
        public virtual Level Level { get; set; }
        public Int64 LevelId { get; set; }

        public virtual List<Student> Students { get; set; }
        public virtual List<Schedule> Schedules { get; set; }
        public virtual List<Exam> Exams { get; set; }

        public Class()
        {
            Students = new List<Student>();
            Schedules = new List<Schedule>();
            Exams = new List<Exam>();
        }
    }
}
