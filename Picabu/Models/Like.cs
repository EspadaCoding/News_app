using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Picabu.Models
{
    [Table("Like")]
    public class Like
    {
        public int Id { get; set; }
        public int NewsId { get; set; }
        public string UserID { get; set; }
        public DateTime LikedDate { get; set; }
        public bool Liked { get; set; }
    }
}
