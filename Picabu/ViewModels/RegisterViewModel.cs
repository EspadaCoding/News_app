using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Picabu.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string FullName { get; set; } 
        [Required]
        [DataType(DataType.Password)]
        [Compare("PasswordAgain")]
        public string Password { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string PasswordAgain { get; set; }
        [Required]
        public int Year { get; set; }
    }
}
