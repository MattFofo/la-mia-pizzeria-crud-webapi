using la_mia_pizzeria.DataBase;
using la_mia_pizzeria.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace la_mia_pizzeria.Controllers
{
    [Authorize]
    public class UserMessageController : Controller
    {
        // GET: UserMessageController
        public ActionResult Index()
        {
            using (PizzeriaContext ctx = new PizzeriaContext())
            {
                List<UserMessage> messages = ctx.UserMessages.ToList();

                return View(messages);
            }

                
        }

        // GET: UserMessageController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserMessageController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserMessageController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserMessageController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserMessageController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserMessageController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserMessageController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
