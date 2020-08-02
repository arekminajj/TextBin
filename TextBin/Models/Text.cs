using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TextBin.Models
{
    public class Text
    {
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string Title { get; set; }
        public DateTime Created { get; set; }
        public string StringId { get; set; }
    }
}
