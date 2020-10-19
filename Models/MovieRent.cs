using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
    public class MovieRent
    {
        [Key]
        public int CustomerId { get; set; }

        [Required]

        public string CustomerName { get; set; }
        [Required]
        [Display(Name = "Date of Return")]

        public DateTime ReturnDate { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public string MovieName { get; set; }
        public Movie Movie { get; set; }
        [Required]
        public int MovieId { get; set; }
       
    }
}
