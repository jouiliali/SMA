using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMA.Entities.Models
{
    public partial class Subject : BaseEntity
    {
        public string Libel { get; set; }

        public virtual List<SubjectLevel> SubjectLevels { get; set; }

        public Subject()
        {
            SubjectLevels = new List<SubjectLevel>();
        }
    }
}
