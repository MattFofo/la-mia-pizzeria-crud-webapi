using la_mia_pizzeria.DataBase;
using la_mia_pizzeria.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace la_mia_pizzeria.Controllers.Api
{
    [Route("api/pizzas")]
    [ApiController]
    public class PizzasController : ControllerBase
    {

        public ActionResult Get(string? search)
        {

            PizzeriaContext ctx = new PizzeriaContext();

            IQueryable<Pizza> pizzasList = ctx.Pizzas;

            if(search != null && search != "")
            {
                pizzasList = ctx.Pizzas.Where(p => p.Name.Contains(search));
            }
            
            

            return Ok(pizzasList.ToList());
  
        }
    }
}
