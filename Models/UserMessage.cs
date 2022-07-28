using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace la_mia_pizzeria.Models
{
    public class UserMessage
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Email richiesta!")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Titolo richiesto!")]
        public string Title { get; set; }
        [Column(TypeName = "text")]
        [Required(ErrorMessage = "Messaggio richiesto!")]
        [StringLength(500, ErrorMessage = "Il testo non può essere così lungo!")]
        public string Text { get; set; }


        public UserMessage(string email, string title, string text)
        {
            Email = email;
            Title = title;
            Text = text;
        }
    }
}
