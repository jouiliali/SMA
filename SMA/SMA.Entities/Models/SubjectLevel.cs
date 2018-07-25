using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace SMA.Entities.Models
{
    public partial class SubjectLevel : BaseEntity
    {
        public int Coef { get; set; }

        [ForeignKey("SubjectId")]
        public virtual Subject Subject { get; set; }
        public Int64 SubjectId { get; set; }

        [ForeignKey("LevelId")]
        public virtual Level Level { get; set; }
        public Int64 LevelId { get; set; }

        public virtual List<Schedule> Schedules { get; set; }

        public SubjectLevel()
        {
            Schedules = new List<Schedule>();
        }
    }
}
