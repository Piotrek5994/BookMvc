using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Enums
{
    public enum Category
    {
        [Display(Name = "Akcja")] Action = 1,
        [Display(Name = "Fantazy")] Fantasies = 2,
        [Display(Name = "Horror")] Horror = 3,
        [Display(Name = "Romans")] Romance = 4
    }
}
