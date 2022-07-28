using la_mia_pizzeria.DataBase;
using la_mia_pizzeria.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace la_mia_pizzeria.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserMessagesController : ControllerBase
    {

        // POST api/<ValuesController>
        [HttpPost]
        public IActionResult Post([FromBody] UserMessage message)
        {
            try
            {
                PizzeriaContext ctx = new PizzeriaContext();

                ctx.UserMessages.Add(message);
                ctx.SaveChanges();

                return Ok(new {Status = "ok", Message = "Dati inseriti correttamente"});

            }
            catch (Exception)
            {

                return BadRequest(new { Status = "ko", Message = "i dati inseriti non sono validi" });
            }
        }

    }
}
