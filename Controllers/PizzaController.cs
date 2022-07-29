using la_mia_pizzeria.DataBase;
using la_mia_pizzeria.Models;
using la_mia_pizzeria.Models.Repositories;
using la_mia_pizzeria.Models.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria.Controllers
{
    
    public class PizzaController : Controller
    {
        private IPizzaRepository PizzaRepository;

        public PizzaController(IPizzaRepository dbPostRepository)
        {
            PizzaRepository = dbPostRepository;
        }



        // GET: PizzasController
        public ActionResult Index()
        {

            List<Pizza> listPizzas = PizzaRepository.GetList();

            return View(listPizzas);
            
        }

        // GET: PizzasController/Details/5
        public ActionResult Details(int id)
        {
            
            Pizza pizza = PizzaRepository.GetById(id);

            if(pizza == null) return NotFound();

            return View(pizza);
            
        }

        [Authorize]
        public ActionResult Create()
        {
            PizzaPivotCrud pizzaPivotCrud = PizzaRepository.Create();

            return View(pizzaPivotCrud);
            

        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PizzaPivotCrud formData)
        {
            if (!ModelState.IsValid)
            {
                using (PizzeriaContext context = new PizzeriaContext())
                {
                    PizzaPivotCrud model = new PizzaPivotCrud();

                    model.Categories = context.Categories.ToList();
                    model.Pizza = formData.Pizza;
                    model.Ingredients = GetIngredientsList();

                    return View(model);
                }
            }

            PizzaRepository.Create(formData);

            return RedirectToAction(nameof(Index));

           
        }

        [Authorize]
        public ActionResult Edit(int id)
        {

            Pizza pizza = PizzaRepository.GetById(id);

            if (pizza == null) return NotFound();

            PizzaPivotCrud model = PizzaRepository.Update(id);

            return View(model);

        }

        [Authorize]
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

            Pizza pizzaToEdit = PizzaRepository.GetById(id);

            if (pizzaToEdit == null) return NotFound();

            PizzaRepository.Update(id, model);

            return RedirectToAction("Index");

        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            if (!ModelState.IsValid) return NotFound();


            Pizza pizza = PizzaRepository.GetById(id);

            if (pizza == null) return NotFound();

            PizzaRepository.Delete(id);

            return RedirectToAction("Index");
        }


        private static List<SelectListItem> GetIngredientsList()
        {

            List<SelectListItem> ingredientsList = IPizzaRepository.GetIngredientsList();

            return ingredientsList;
        }
    }
}
