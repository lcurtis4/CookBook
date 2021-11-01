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
    }
}
