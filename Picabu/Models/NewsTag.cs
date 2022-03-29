using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Picabu.Models
{
    [Table("NewsTag")]
    public class NewsTag
    {
        public int Id { get; set; }
        public int NewsId { get; set; }
        public virtual News News { get; set; }
        public int TagId { get; set; }
        public virtual Tag Tag { get; set; }

    }
}
