using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_TiketsMovie.Models.Tables.Peopel
{
    public class AppliationUser:IdentityUser
    {
        [Required,Display(Name ="Full Name")]
        public string FullName { get; set; }

        public ICollection<Order>  Orders { get; set; }
    }
}
