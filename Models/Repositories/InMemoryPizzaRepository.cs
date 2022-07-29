using la_mia_pizzeria.Models.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace la_mia_pizzeria.Models.Repositories
{
    public class InMemoryPizzaRepository : IPizzaRepository
    {
        private static List<Ingredient> Ingredients = new List<Ingredient>()
        { 
            new Ingredient() { Name = "pomodoro" },
            new Ingredient() { Name = "mozzarella" },
            new Ingredient() { Name = "prosciutto" },
            new Ingredient() { Name = "funghi" },
        };

        private static List<Category> Categories = new List<Category>()
        {
            new Category() { Name = "pizze speciali" },
            new Category() { Name = "pizze bianche" },
            new Category() { Name = "pizze di mare" },
            new Category() { Name = "pizze brutte" },
        };

        private static List<Pizza> Pizzas = new List<Pizza>();

        public InMemoryPizzaRepository()
        {
            Pizza pizza1 = new Pizza("Margherita", "Molto buona", "https://picsum.photos/200", 8.50m)
            {
                Category = new Category() { Name = "Pizze bianche" },

                Ingredients = new List<Ingredient> { new Ingredient() { Name = "mozzarella"}, new Ingredient() { Name = "pomodoro" } }  
            };
            Pizza pizza2 = new Pizza("Prosciutto", "Ancora più buona", "https://picsum.photos/200", 9.50m)
            {
                Category = new Category() { Name = "Pizze speciali" },

                Ingredients = new List<Ingredient> { new Ingredient() { Name = "prosciutto" }, new Ingredient() { Name = "pomodoro" } }
            };


            Pizzas.Add(pizza1);
            Pizzas.Add(pizza2);
        }


        public static List<SelectListItem> GetIngredientsList()
        {

            List<SelectListItem> ingredientsList = new List<SelectListItem>();

            List<Ingredient> ingredients = Ingredients;

            foreach (Ingredient ingredient in ingredients)
            {
                ingredientsList.Add(new SelectListItem { Text = ingredient.Name, Value = ingredient.Id.ToString() });
            }

            return ingredientsList;            
        }


        public PizzaPivotCrud Create()
        {
            throw new NotImplementedException();
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

                    Ingredient ingredient = Ingredients.Where(ingredient => ingredient.Id == selectedIntIngredientId).FirstOrDefault();

                    if(ingredient != null) newPizza.Ingredients.Add(ingredient);
                    
                }
            }

            Pizzas.Add(newPizza);
        }

        public void Delete(int id)
        {
            Pizza pizzaToDelete = Pizzas.Find(p => p.Id == id);

            if (pizzaToDelete != null)
            {
                Pizzas.Remove(pizzaToDelete);
            }
        }

        public Pizza GetById(int id)
        {
            Pizza pizza = Pizzas.Find(p => p.Id == id);

            return pizza;
            
        }

        public List<Pizza> GetList()
        {
            return Pizzas;
        }

        public List<Pizza> GetListByFilter(string search)
        {
            List<Pizza> pizzasList = Pizzas.Where(p => p.Name.Contains(search)).ToList();

            return pizzasList;
        }

        public PizzaPivotCrud Update(int id)
        {
            Pizza pizza = Pizzas.Where(p => p.Id == id).FirstOrDefault();

            List<Category> categories = Categories;

            PizzaPivotCrud model = new PizzaPivotCrud();
            model.Pizza = pizza;
            model.Categories = Categories; //????
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
            Pizza pizzaToEdit = Pizzas.Where(p => p.Id == id).FirstOrDefault();

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

                    Ingredient ingredient = Ingredients.Where(ingredient => ingredient.Id == selectedIntIngredientId).FirstOrDefault();

                    pizzaToEdit.Ingredients.Add(ingredient);
                }
            }
        }
    }
}
