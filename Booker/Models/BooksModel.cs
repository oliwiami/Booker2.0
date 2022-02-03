using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Booker.Models
{   
    [Table("books")]
    public class Books
    {
        [Key]
        public int BookId { get; set; }

         
        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string Publisher { get; set; }

        [DisplayName("Publish Date")]
        [Required]
        [DisplayFormat(DataFormatString ="{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Publishdate { get; set; }

        [Required]
        [Range(0,101, ErrorMessage ="No.")]
        public int Quantity { get; set; }

    }
}
