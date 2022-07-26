using la_mia_pizzeria.DataBase;
using la_mia_pizzeria.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria.Controllers
{
    [Authorize]
    public class PizzaController : Controller
    {
        // GET: PizzasController
        public ActionResult Index()
        {
            using (PizzeriaContext context = new PizzeriaContext())
            {

                List<Pizza> listPizzas = context.Pizzas.Include(p => p.Category).ToList();

                return View(listPizzas);
            }

        }

        // GET: PizzasController/Details/5
        public ActionResult Details(int id)
        {
            using(PizzeriaContext context = new PizzeriaContext())
            {
                Pizza pizza = context.Pizzas.Where(pizza => pizza.Id == id).Include(p => p.Category).Include(p => p.Ingredients).FirstOrDefault();

                return View(pizza);
            }
        }

        // GET: PizzasController/Create
        public ActionResult Create()
        {
            using (PizzeriaContext context = new PizzeriaContext())
            {

                List<Category> categories = context.Categories.ToList();

                PizzaPivotCrud pizzaPivotCrud = new PizzaPivotCrud();

                pizzaPivotCrud.Categories = categories;
                pizzaPivotCrud.Pizza = new Pizza();

                pizzaPivotCrud.Ingredients = GetIngredientsList();

                return View(pizzaPivotCrud);
            }

        }


        // POST: PizzasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PizzaPivotCrud formData)
        {
            using (PizzeriaContext context = new PizzeriaContext())
            {
                if (!ModelState.IsValid)
                {
                    PizzaPivotCrud model = new PizzaPivotCrud();

                    model.Categories = context.Categories.ToList();
                    model.Pizza = formData.Pizza;
                    model.Ingredients = GetIngredientsList();

                    return View(model);
                }

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

                        Ingredient ingredient = context.Ingredients.Where(ingredient => ingredient.Id == selectedIntIngredientId).FirstOrDefault();

                        newPizza.Ingredients.Add(ingredient);
                    }
                }

                context.Pizzas.Add(newPizza);
                context.SaveChanges();

                return RedirectToAction(nameof(Index));

            }
        }

        // GET: PizzasController/Edit/5
        public ActionResult Edit(int id)
        {
            using (PizzeriaContext context = new PizzeriaContext())
            {
                Pizza pizza = context.Pizzas.Where(p => p.Id == id).Include(p => p.Category).Include(p => p.Ingredients).FirstOrDefault();

                if (pizza == null) return NotFound();

                List<Category> categories = context.Categories.ToList();

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

                return View(model);
            }

        }

        // POST: PizzasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PizzaPivotCrud model)
        {
            if (!ModelState.IsValid)
            {
                using (PizzeriaContext context = new PizzeriaContext())
                {

                    model.Categories = context.Categories.ToList();
                    model.Ingredients = GetIngredientsList();

                    return View(model);

                }
            }

            using(PizzeriaContext context = new PizzeriaContext())
            {
                Pizza pizzaToEdit = context.Pizzas.Where(p => p.Id == id).Include(p => p.Ingredients).FirstOrDefault();

                if (pizzaToEdit == null) return NotFound();

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

                        Ingredient ingredient = context.Ingredients.Where(ingredient => ingredient.Id == selectedIntIngredientId).FirstOrDefault();

                        pizzaToEdit.Ingredients.Add(ingredient);
                    }
                }

                //context.Update(pizzaToEdit);
                context.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        // POST: PizzasController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            if (!ModelState.IsValid) return NotFound();

            using (PizzeriaContext context = new PizzeriaContext())
            {
                Pizza pizza = context.Pizzas.Where(p => p.Id == id).FirstOrDefault();

                if (pizza == null) return NotFound();

                context.Pizzas.Remove(pizza);
                context.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        private static List<SelectListItem> GetIngredientsList()
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
    }
}
