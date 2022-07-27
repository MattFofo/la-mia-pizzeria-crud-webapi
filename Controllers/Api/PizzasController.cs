using la_mia_pizzeria.DataBase;
using la_mia_pizzeria.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria.Controllers.Api
{
    [Route("api/pizzas")]
    [ApiController]
    public class PizzasController : ControllerBase
    {

        public ActionResult Get(string? search)
        {

            PizzeriaContext ctx = new PizzeriaContext();

            IQueryable<Pizza> pizzasList = ctx.Pizzas.Include("Category");

            if(search != null && search != "")
            {
                pizzasList = ctx.Pizzas.Where(p => p.Name.Contains(search)).Include("Category");
            }
            
            

            return Ok(pizzasList.ToList());
  
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {

            PizzeriaContext ctx = new PizzeriaContext();

            Pizza pizza = ctx.Pizzas.Where(p => p.Id == id).Include("Category").Include("Ingredients").FirstOrDefault();

            if(pizza == null) return NotFound();

            return Ok(pizza);

        }
    }
}
