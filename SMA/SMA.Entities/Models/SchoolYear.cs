using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMA.Entities.Models
{
    public partial class SchoolYear : BaseEntity
    {
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }

        public virtual List<Schedule> Schedules { get; set; }
        public virtual List<Exam> Exams { get; set; }
        public virtual List<Punishment> Punishments { get; set; }
        public virtual List<Mark> Marks { get; set; }
        public virtual List<Attendance> Attendances { get; set; }

        public SchoolYear()
        {
            Schedules = new List<Schedule>();
            Exams = new List<Exam>();
            Punishments = new List<Punishment>();
            Marks = new List<Mark>();
            Attendances = new List<Attendance>();
        }
    }
}
