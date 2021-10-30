using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using CookBook.Models;
using CookBook.Utils;

namespace CookBook.Repositories
{
    public class DishRepository : BaseRepository, IDishRepository
    {
        public DishRepository(IConfiguration config) : base(config) { }

        public List<Dish> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT d.Id, d.Title, d.CreateDateTime, d.UserProfileId, u.[Name]
                                   FROM Dish d
                                         LEFT JOIN UserProfile u ON d.UserProfileId = u.id";
                    var reader = cmd.ExecuteReader();
                    var dish = new List<Dish>();
                    while (reader.Read())
                    {
                        dish.Add(new Dish()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Title = DbUtils.GetString(reader, "Title"),
                            CreateDateTime = DbUtils.GetDateTime(reader, "CreateDateTime"),
                            UserProfileId = DbUtils.GetInt(reader, "UserProfileId"),
                            UserProfile = new UserProfile()
                            {
                                Id = DbUtils.GetInt(reader, "UserProfileId"),
                                Name = DbUtils.GetString(reader, "Name")
                            },
                        });
                    }
                    reader.Close();

                    return dish; 
                }
            }
        }

        public Dish GetByDishId(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT d.Id, d.Title, d.CreateDateTime, d.UserProfileId, u.[Name]
                                   FROM Dish d
                                         LEFT JOIN UserProfile u ON d.UserProfileId = u.id
                                    WHERE d.id = @id";

                    DbUtils.AddParameter(cmd, "@Id", id);
                    var reader = cmd.ExecuteReader();
                    Dish dish = null;
                    if (reader.Read())
                    {
                        dish = new Dish()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Title = DbUtils.GetString(reader, "Title"),
                            CreateDateTime = DbUtils.GetDateTime(reader, "CreateDateTime"),
                            UserProfileId = DbUtils.GetInt(reader, "UserProfileId"),
                            UserProfile = new UserProfile()
                            {
                                Id = DbUtils.GetInt(reader, "UserProfileId"),
                                Name = DbUtils.GetString(reader, "Name")
                            },
                        };
                    }
                    reader.Close();

                    return dish;
                }
            }
        }

        public void Add(Dish dish)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Dish (Title, Image, UserProfileId, CategoryId, CreateDateTime)
                                        OUTPUT INSERTED.ID
                                        VALUES (@Title, @Image, @UserProfileId, 1, SYSDATETIME())";
                    DbUtils.AddParameter(cmd, "@Title", dish.Title);
                    DbUtils.AddParameter(cmd, "@Image", dish.Image);
                    DbUtils.AddParameter(cmd, "@UserProfileId", dish.UserProfileId);
                    DbUtils.AddParameter(cmd, "@CategoryId", dish.CategoryId);

                    dish.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void Delete(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Dish WHERE @Id = Id";
                    DbUtils.AddParameter(cmd, "@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
