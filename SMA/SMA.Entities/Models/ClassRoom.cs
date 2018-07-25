using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMA.Entities.Models
{
    public partial class ClassRoom : BaseEntity
    {
        public string Libel { get; set; }

        public virtual List<Schedule> Schedules { get; set; }
        public virtual List<Exam> Exams { get; set; }

        public ClassRoom()
        {
            Schedules = new List<Schedule>();
            Exams = new List<Exam>();
        }
    }
}
