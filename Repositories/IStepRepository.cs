using CookBook.Models;

namespace CookBook.Repositories
{
    public interface IStepRepository
    {
        Step GetStepByDishId(int id);
    }
}