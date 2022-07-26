using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        //relazioni esterne
        List<Pizza> Pizzas { get; set; }

        public Category()
        {

        }
    }
}
