using la_mia_pizzeria.DataBase;
using la_mia_pizzeria.Models;
using la_mia_pizzeria.Models.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria.Controllers.Api
{
    [Route("api/pizzas")]
    [ApiController]
    public class PizzasController : ControllerBase
    {
        private DbPizzaRepository DbPizzaRepository;

        public PizzasController(DbPizzaRepository dbPizzaRepository)
        {
            DbPizzaRepository = dbPizzaRepository;
        }

        public ActionResult Get(string? search)
        {

            List<Pizza> pizzasList = DbPizzaRepository.GetList();

            if(search != null && search != "")
            {
                pizzasList = DbPizzaRepository.GetListByFilter(search);
            }
            
            return Ok(pizzasList.ToList());
  
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {

            Pizza pizza = DbPizzaRepository.GetById(id);

            if(pizza == null) return NotFound();

            return Ok(pizza);

        }
    }
}
