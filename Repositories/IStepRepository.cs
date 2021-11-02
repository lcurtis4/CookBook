using CookBook.Models;
using System.Collections.Generic;

namespace CookBook.Repositories
{
    public interface IStepRepository
    {
        Step GetStepByDishId(int id);
        List<Step> GetAll(int id);
        void Add(Step step);
    }
}