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
                    cmd.CommandText = @"SELECT d.Id, d.Title, d.CreateDateTime, d.UserProfileId, s.Id StepId, s.stepText, s.stepOrder
                                   FROM Dish d
                                         LEFT JOIN Step s ON d.id = s.dishId
                                    WHERE d.id = @id
                                    ORDER BY s.stepOrder";

                    DbUtils.AddParameter(cmd, "@Id", id);
                    var reader = cmd.ExecuteReader();
                    Dish dish = null;
                    while (reader.Read())
                    {
                        if (dish == null)
                        {
                            dish = new Dish()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Title = DbUtils.GetString(reader, "Title"),
                                CreateDateTime = DbUtils.GetDateTime(reader, "CreateDateTime"),
                                UserProfileId = DbUtils.GetInt(reader, "UserProfileId"),
                                Steps = new List<Step>()
                            };
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("StepId")))
                        {
                            dish.Steps.Add(new Step
                            {
                                Id = DbUtils.GetInt(reader, "StepId"),
                                stepText = DbUtils.GetString(reader, "stepText"),
                                stepOrder = DbUtils.GetInt(reader, "stepOrder")
                            });
                        }
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

        public void Update(Dish dish)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                        UPDATE Dish
                                        SET
                                            Title = @Title,
                                            Image = @Image
                                         WHERE
                                            Id = @Id";
                    DbUtils.AddParameter(cmd, "@Title", dish.Title);
                    DbUtils.AddParameter(cmd, "@Image", dish.Image);
                    DbUtils.AddParameter(cmd, "@Id", dish.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
