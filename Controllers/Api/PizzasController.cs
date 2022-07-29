using la_mia_pizzeria.DataBase;
using la_mia_pizzeria.Models;
using la_mia_pizzeria.Models.Repositories;
using la_mia_pizzeria.Models.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria.Controllers.Api
{
    [Route("api/pizzas")]
    [ApiController]
    public class PizzasController : ControllerBase
    {
        private IPizzaRepository PizzaRepository;

        public PizzasController(IPizzaRepository dbPizzaRepository)
        {
            PizzaRepository = dbPizzaRepository;
        }

        public ActionResult Get(string? search)
        {

            List<Pizza> pizzasList = PizzaRepository.GetList();

            if(search != null && search != "")
            {
                pizzasList = PizzaRepository.GetListByFilter(search);
            }
            
            return Ok(pizzasList.ToList());
  
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {

            Pizza pizza = PizzaRepository.GetById(id);

            if(pizza == null) return NotFound();

            return Ok(pizza);

        }
    }
}
