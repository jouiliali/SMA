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
    public partial class SchoolHead : UserSMA
    {
        
        public virtual List<Holiday> Holidays { get; set; }

        public SchoolHead()
        {
            Holidays = new List<Holiday>();
        }
    }
}
