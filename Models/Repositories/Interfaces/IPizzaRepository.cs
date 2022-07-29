using Microsoft.AspNetCore.Mvc.Rendering;

namespace la_mia_pizzeria.Models.Repositories.Interfaces
{
    public interface IPizzaRepository
    {
        PizzaPivotCrud Create();
        void Create(PizzaPivotCrud formData);
        void Delete(int id);
        Pizza GetById(int id);
        List<Pizza> GetList();
        List<Pizza> GetListByFilter(string search);
        PizzaPivotCrud Update(int id);
        void Update(int id, PizzaPivotCrud model);

        static List<SelectListItem> GetIngredientsList() //TODO
        {
            var list = new List<SelectListItem>();
            return list;
        }
    }
}