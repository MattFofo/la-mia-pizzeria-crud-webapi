using la_mia_pizzeria.DataBase;
using la_mia_pizzeria.Models;
using la_mia_pizzeria.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria.Controllers
{
    
    public class PizzaController : Controller
    {
        private DbPizzaRepository DbPizzaRepository { get; set; }

        public PizzaController(DbPizzaRepository dbPostRepository)
        {
            DbPizzaRepository = dbPostRepository;
        }



        // GET: PizzasController
        public ActionResult Index()
        {

            List<Pizza> listPizzas = DbPizzaRepository.GetList();

            return View(listPizzas);
            
        }

        // GET: PizzasController/Details/5
        public ActionResult Details(int id)
        {
            
            Pizza pizza = DbPizzaRepository.GetById(id);

            if(pizza == null) return NotFound();

            return View(pizza);
            
        }

        [Authorize]
        public ActionResult Create()
        {
            PizzaPivotCrud pizzaPivotCrud = DbPizzaRepository.Create();

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

            DbPizzaRepository.Create(formData);

            return RedirectToAction(nameof(Index));

           
        }

        [Authorize]
        public ActionResult Edit(int id)
        {

            Pizza pizza = DbPizzaRepository.GetById(id);

            if (pizza == null) return NotFound();

            PizzaPivotCrud model = DbPizzaRepository.Update(id);

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

            Pizza pizzaToEdit = DbPizzaRepository.GetById(id);

            if (pizzaToEdit == null) return NotFound();

            DbPizzaRepository.Update(id, model);

            return RedirectToAction("Index");

        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            if (!ModelState.IsValid) return NotFound();


            Pizza pizza = DbPizzaRepository.GetById(id);

            if (pizza == null) return NotFound();

            DbPizzaRepository.Delete(id);

            return RedirectToAction("Index");
        }


        private static List<SelectListItem> GetIngredientsList()
        {

            List<SelectListItem> ingredientsList = DbPizzaRepository.GetIngredientsList();

            return ingredientsList;
        }
    }
}
