using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMA.Entities.Models
{
    public class BaseEntity<TKey> : Entity
    {
        [Key]
        public virtual TKey Id { get; set; }
    }

    public class BaseEntity : BaseEntity<long>
    {

    }
}
