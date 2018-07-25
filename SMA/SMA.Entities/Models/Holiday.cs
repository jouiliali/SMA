using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace SMA.Entities.Models
{
    public partial class Holiday : BaseEntity
    {
        public string Libel { get; set; }
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }

        [ForeignKey("SchoolHeadId")]
        public virtual SchoolHead SchoolHead { get; set; }
        public Int64 SchoolHeadId { get; set; }
    }
}
