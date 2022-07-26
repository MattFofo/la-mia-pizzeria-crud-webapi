using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria.Models
{
    public class Ingredient
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Il nome non può eccedere i 50 caratteri!")]
        public string Name { get; set; }

        //relazioni esterne

        public List<Pizza> Pizzas { get; set; }

        public Ingredient()
        {

        }
    }
}
