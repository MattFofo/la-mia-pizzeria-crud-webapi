using Microsoft.AspNetCore.Mvc.Rendering;

namespace la_mia_pizzeria.Models
{
    public class PizzaPivotCrud
    {
        public Pizza Pizza { get; set; }

        public List<Category>? Categories { get; set; }

        public List<SelectListItem>? Ingredients { get; set; }

        public List<string>? SelectedIngredients { get; set; }

        public PizzaPivotCrud()
        {

        }
    }
}
