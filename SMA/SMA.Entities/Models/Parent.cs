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
    public partial class Parent : UserSMA
    {
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Adress{ get; set; }
        public string Tel { get; set; }
        public string Mobile { get; set; }
        public string Profession { get; set; }

        public virtual List<Message> Messages { get; set; }
        public virtual List<Testimonial> Testimonials { get; set; }
        public virtual List<ParentHasStudent> ParentHasStudent { get; set; }

        public Parent()
        {
            Messages = new List<Message>();
            Testimonials = new List<Testimonial>();
            ParentHasStudent = new List<ParentHasStudent>();
        }
    }
}
