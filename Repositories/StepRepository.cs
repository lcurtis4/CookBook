using CookBook.Models;
using CookBook.Utils;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookBook.Repositories
{
    public class StepRepository : BaseRepository, IStepRepository
    {
        public StepRepository(IConfiguration config) : base(config) { }

        public List<Step> GetAll(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT s.stepText, s.stepOrder, s.dishId, d.id, d.Title
                               FROM Step s
                                       LEFT JOIN Dish d ON s.dishId = d.id
                                WHERE s.dishId = @Id";

                    DbUtils.AddParameter(cmd, "@Id", id);
                    
                    var reader = cmd.ExecuteReader();
                    var step = new List<Step>();
                    while (reader.Read())
                    {
                        step.Add(new Step()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            dishId = DbUtils.GetInt(reader, "dishId"),
                            stepOrder = DbUtils.GetInt(reader, "stepOrder"),
                            stepText = DbUtils.GetString(reader, "stepText"),
                            Dish = new Dish()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Title = DbUtils.GetString(reader, "Title")
                            }
                        });
                    }
                    reader.Close();

                    return step;
                }
            }
        }
        public Step GetStepByDishId(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT s.stepText, s.stepOrder, s.dishId, d.id, d.Title
                               FROM Step s
                                        JOIN Dish d ON s.dishId = d.id
                                WHERE s.dishId = d.id";

                    DbUtils.AddParameter(cmd, "@Id", id);

                    var reader = cmd.ExecuteReader();

                    Step step = null;

                    if (reader.Read())
                    {
                        step = new Step()
                        {
                            Id = id,
                            stepOrder = DbUtils.GetInt(reader, "stepOrder"),
                            stepText = DbUtils.GetString(reader, "stepText"),
                            dishId = DbUtils.GetInt(reader, "dishId")
                        };
                    }
                    reader.Close();

                    return step;
                }

            }
        }

        public void Add(Step step)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Step (dishId, stepText, stepOrder)
                                        OUTPUT INSERTED.ID
                                        VALUES (@dishId, @stepText, @stepOrder)";
                    DbUtils.AddParameter(cmd, "@stepText", step.stepText);
                    DbUtils.AddParameter(cmd, "@stepOrder", step.stepOrder);
                    DbUtils.AddParameter(cmd, "@dishId", step.dishId);

                    int id = (int)cmd.ExecuteScalar();
                    step.Id = id; 
                }
            }
        }


    }
}
