using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]

        public string Name { get; set; }
        [Required]
        [Display(Name = "Date of Realease")]

        public DateTime RealeaseDate { get; set; }
        [Required]
        public int NumberInStocks { get; set; }
        [Required]
        public int Price { get; set; }
        public Genre Genre { get; set; }
        [Required]
        public int GenreId { get; set; }
    }
}
