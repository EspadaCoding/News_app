using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Picabu.Models.Identity
{
    public class AppUser : IdentityUser
    { 
        public virtual string Fullname { get; set; }
        public virtual int Year { get; set; } 

    }
}
