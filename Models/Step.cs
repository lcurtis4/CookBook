using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookBook.Models
{
    public class Step
    {
        public int Id { get; set; }
        public int dishId { get; set; }
        public int stepOrder { get; set; }
        public string stepText { get; set; }
    }
}
