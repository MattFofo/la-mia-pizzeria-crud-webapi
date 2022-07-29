using la_mia_pizzeria.Models.Repositories.Interfaces;

namespace la_mia_pizzeria.Models.Repositories
{
    public class InMemoryPizzaRepository : IPizzaRepository
    {

        private static List<Pizza> Pizzas = new List<Pizza>();

        public InMemoryPizzaRepository()
        {
            Pizza pizza1 = new Pizza("Margherita", "Molto buona", "https://picsum.photos/200", 8.50m)
            {
                Category = new Category() { Name = "Pizze bianche" },

                Ingredients = new List<Ingredient> { new Ingredient() { Name = "mozzarella"}, new Ingredient() { Name = "Pomodoro" } }  
            };
            Pizza pizza2 = new Pizza("Prosciutto", "Ancora più buona", "https://picsum.photos/200", 9.50m)
            {
                Category = new Category() { Name = "Pizze speciali" },

                Ingredients = new List<Ingredient> { new Ingredient() { Name = "prosciutto" }, new Ingredient() { Name = "Pomodoro" } }
            };


            Pizzas.Add(pizza1);
            Pizzas.Add(pizza2);
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

            //if (formData.SelectedIngredients != null)
            //{
            //    foreach (string selectedIngredientId in formData.SelectedIngredients)
            //    {
            //        int selectedIntIngredientId = int.Parse(selectedIngredientId);

            //        Ingredient ingredient = _context.Ingredients.Where(ingredient => ingredient.Id == selectedIntIngredientId).FirstOrDefault();

            //        newPizza.Ingredients.Add(ingredient);
            //    }
            //}

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
            throw new NotImplementedException();
        }

        public void Update(int id, PizzaPivotCrud model)
        {
            throw new NotImplementedException();
        }
    }
}
