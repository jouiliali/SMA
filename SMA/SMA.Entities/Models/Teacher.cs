using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace SMA.Entities.Models
{
    public partial class Teacher : UserSMA
    {
        public bool Leave { get; set; }

        public virtual List<Schedule> Schedules { get; set; }
        public virtual List<Testimonial> Testimonials { get; set; }
        public virtual List<Mark> Marks { get; set; }
        public virtual List<Punishment> Punishments { get; set; }

        public Teacher()
        {
            Schedules = new List<Schedule>();
            Testimonials = new List<Testimonial>();
            Marks = new List<Mark>();
            Punishments = new List<Punishment>();
        }

    }
}
