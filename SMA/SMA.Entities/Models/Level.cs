using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMA.Entities.Models
{
    public partial class Level : BaseEntity
    {
        public string Libel { get; set; }

        public virtual List<SubjectLevel> SubjectLevels { get; set; }
        public virtual List<Class> Classes { get; set; }

        public Level()
        {
            SubjectLevels = new List<SubjectLevel>();
            Classes = new List<Class>();
        }
    }
}
