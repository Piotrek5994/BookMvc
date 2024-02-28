using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class User : IdentityUser<int>
    {
        [StringLength(100)]
        [Required]
        public string? Name { get; set; }

        public string? Address { get; set; }
    }
}
