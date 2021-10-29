using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CookBook.Models
{
    public class Dish
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        public string Image { get; set; }
        
        [Required]
        public DateTime CreateDateTime { get; set; }
        public int CategoryId { get; set; }

        [Required]
        public int UserProfileId { get; set; }
        
        public UserProfile UserProfile { get; set; }
    }
}
