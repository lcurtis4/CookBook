using CookBook.Models;
using System.Collections.Generic;

namespace CookBook.Repositories
{
    public interface IDishRepository
    {
        List<Dish> GetAll();

        Dish GetByDishId(int id);
        void Add(Dish dish);
        void Delete(int id);
        void Update(Dish dish);
    }
}