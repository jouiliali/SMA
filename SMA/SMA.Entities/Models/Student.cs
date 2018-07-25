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
    public partial class Student : BaseEntity
    {
      
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool DropOut { get; set; }
        public bool Live { get; set; }
        public string Genre { get; set; }
        public string LieuNaissance { get; set; }
        public DateTime dateBirth { get; set; }
        public string tel { get; set; }
        public string portable { get; set; }
        public string adresse { get; set; }

        [ForeignKey("ClassId")]
        public virtual Class Class { get; set; }
        public Int64 ClassId { get; set; }

        [ForeignKey("ImageId")]
        public virtual Image Image { get; set; }
        public Int64 ImageId { get; set; }

        public virtual List<Mark> Marks { get; set; }
        public virtual List<Punishment> Punishments { get; set; }
        public virtual List<Attendance> Attendances { get; set; }
        public virtual List<ParentHasStudent> ParentHasStudent { get; set; }
        public virtual List<StudentComment> StudentComments { get; set; }

        public Student()
        {
            Marks = new List<Mark>();
            Punishments = new List<Punishment>();
            Attendances = new List<Attendance>();
            ParentHasStudent = new List<ParentHasStudent>();
            StudentComments = new List<StudentComment>();
        }

        public Student(string Firstname)
        {
            this.FirstName = FirstName;
            Marks = new List<Mark>();
            Punishments = new List<Punishment>();
            Attendances = new List<Attendance>();
            ParentHasStudent = new List<ParentHasStudent>();
            StudentComments = new List<StudentComment>();
        }
    }
}
