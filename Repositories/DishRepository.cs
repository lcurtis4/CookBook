using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using CookBook.Models;
using CookBook.Utils;

namespace CookBook.Repositories
{
    public class DishRepository : BaseRepository, IDishRepository
    {
        public DishRepository(IConfiguration config) : base(config) { }
        public void Add(Dish dish)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Dish (Title, ImageLocation, CreateDateTime, CategoryId, UserProfileId)
                                        OUTPUT INSERTD.ID
                                        VALUES (@Title, @ImageLocation, @CreateDateTime, @CategoryId, @UserProfileId";
                    DbUtils.AddParameter(cmd, "@Title", dish.Title);
                    DbUtils.AddParameter(cmd, "@ImageLocation", dish.ImageLocation);
                    DbUtils.AddParameter(cmd, "@CreateDateTime", dish.CreateDateTime);
                    DbUtils.AddParameter(cmd, "@UserProfileId", dish.UserProfileId);

                    dish.Id = (int)cmd.ExecuteScalar();
                }
            }
        }
    }
}
