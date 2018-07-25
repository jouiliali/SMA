using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMA.Entities.Models
{
    public partial class Term : BaseEntity
    {
        public string Libel { get; set; }
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }

        public virtual List<Assessment> Assessments { get; set; }

        public Term()
        {
            Assessments = new List<Assessment>();
        }
    }
}
