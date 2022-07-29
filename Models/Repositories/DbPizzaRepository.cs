using la_mia_pizzeria.DataBase;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria.Models.Repositories
{
    public class DbPizzaRepository
    {
        private readonly PizzeriaContext _context;

        public DbPizzaRepository(PizzeriaContext context)
        {
            _context = context;
        }

        public static List<SelectListItem> GetIngredientsList()
        {

            using (PizzeriaContext context = new PizzeriaContext())
            {
                List<SelectListItem> ingredientsList = new List<SelectListItem>();

                List<Ingredient> ingredients = context.Ingredients.ToList();

                foreach (Ingredient ingredient in ingredients)
                {
                    ingredientsList.Add(new SelectListItem { Text = ingredient.Name, Value = ingredient.Id.ToString() });
                }


                return ingredientsList;
            }
        }


        public List<Pizza> GetList()
        {

            List<Pizza> listPizzas = _context.Pizzas.Include(p => p.Category).Include(p => p.Ingredients).ToList();

            return listPizzas;
            
        }


        public Pizza GetById(int id)
        {

            Pizza pizza = _context.Pizzas.Where(pizza => pizza.Id == id).Include(p => p.Category).Include(p => p.Ingredients).FirstOrDefault();

            return pizza;

        }


        public PizzaPivotCrud Create()
        {

            List<Category> categories = _context.Categories.ToList();

            PizzaPivotCrud pizzaPivotCrud = new PizzaPivotCrud();

            pizzaPivotCrud.Categories = categories;
            pizzaPivotCrud.Pizza = new Pizza();

            pizzaPivotCrud.Ingredients = GetIngredientsList();

            return pizzaPivotCrud;

        }


        public void Create(PizzaPivotCrud formData)
        {

            Pizza newPizza = new Pizza();
            newPizza.Name = formData.Pizza.Name;
            newPizza.Image = formData.Pizza.Image;
            newPizza.Description = formData.Pizza.Description;
            newPizza.Price = formData.Pizza.Price;
            newPizza.Category = formData.Pizza.Category;
            newPizza.CategoryId = formData.Pizza.CategoryId;

            newPizza.Ingredients = new List<Ingredient>();

            if (formData.SelectedIngredients != null)
            {
                foreach (string selectedIngredientId in formData.SelectedIngredients)
                {
                    int selectedIntIngredientId = int.Parse(selectedIngredientId);

                    Ingredient ingredient = _context.Ingredients.Where(ingredient => ingredient.Id == selectedIntIngredientId).FirstOrDefault();

                    newPizza.Ingredients.Add(ingredient);
                }
            }

            _context.Pizzas.Add(newPizza);
            _context.SaveChanges();
            
        }

        public PizzaPivotCrud Update(int id)
        {

            Pizza pizza = _context.Pizzas.Where(pizza => pizza.Id == id).Include(p => p.Category).Include(p => p.Ingredients).FirstOrDefault();

            List<Category> categories = _context.Categories.ToList();

            PizzaPivotCrud model = new PizzaPivotCrud();
            model.Pizza = pizza;
            model.Categories = categories;
            model.Ingredients = GetIngredientsList();
            model.SelectedIngredients = new List<string>();

            if (pizza.Ingredients != null)
            {
                foreach (Ingredient ingredient in pizza.Ingredients)
                {
                    model.SelectedIngredients.Add(ingredient.Id.ToString());
                }
            }

            return model;
            
        }


        public void Update(int id, PizzaPivotCrud model)
        {

            Pizza pizzaToEdit = this.GetById(id);

            pizzaToEdit.Name = model.Pizza.Name;
            pizzaToEdit.Description = model.Pizza.Description;
            pizzaToEdit.Image = model.Pizza.Image;
            pizzaToEdit.Price = model.Pizza.Price;
            pizzaToEdit.Category = model.Pizza.Category;
            pizzaToEdit.CategoryId = model.Pizza.CategoryId;

            pizzaToEdit.Ingredients.Clear();

            if (model.SelectedIngredients != null)
            {
                foreach (string selectedIngredientId in model.SelectedIngredients)
                {
                    int selectedIntIngredientId = int.Parse(selectedIngredientId);

                    Ingredient ingredient = _context.Ingredients.Where(ingredient => ingredient.Id == selectedIntIngredientId).FirstOrDefault();

                    pizzaToEdit.Ingredients.Add(ingredient);
                }
            }

            _context.Update(pizzaToEdit);
            _context.SaveChanges();
            
        }


        public void Delete(int id)
        {
            Pizza pizza = this.GetById(id);

            _context.Pizzas.Remove(pizza);
            _context.SaveChanges();
 
        }   
    }
}
