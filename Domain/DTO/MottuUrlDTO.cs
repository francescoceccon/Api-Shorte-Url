using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class MottuUrlDTO
    {
        [Key]
        public string Id { get; set; }
        public int Hits { get; set; }
        public string ShortUrl { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Invalid URL")]
        [MinLength(10)]
        public string Url { get; set; }
    }
}
