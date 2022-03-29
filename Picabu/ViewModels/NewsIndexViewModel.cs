using Picabu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Picabu.ViewModels
{
    public class NewsIndexViewModel
    {
        public IEnumerable<News> News { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Comment> Comment { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int CategoryId { get; set; }
    }
}
