using CookBook.Models;
using System.Collections.Generic;

namespace CookBook.Repositories
{
    public interface IDishRepository
    {
        void Add(Dish dish);
        List<Dish> GetAll();

        Dish GetByDishId(int id);

    }
}