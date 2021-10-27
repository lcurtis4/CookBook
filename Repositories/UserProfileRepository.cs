using Microsoft.Extensions.Configuration;
using CookBook.Models;
using CookBook.Utils;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace CookBook.Repositories
{
    public class UserProfileRepository : BaseRepository, IUserProfileRepository
    {
        public UserProfileRepository(IConfiguration configuration) : base(configuration) { }

        public UserProfile GetByFirebaseUserId(string firebaseUserId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                       SELECT Id, [Name], email, CreateDateTime, firebaseUserId
                            FROM UserProfile
                            WHERE firebaseUserId = @FirebaseUserId";

                    DbUtils.AddParameter(cmd, "@FirebaseUserId", firebaseUserId);

                    UserProfile userProfile = null;

                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        userProfile = new UserProfile()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            FirebaseUserId = DbUtils.GetString(reader, "firebaseUserId"),
                            Name = DbUtils.GetString(reader, "Name"),
                            Email = DbUtils.GetString(reader, "email"),
                            CreateDateTime = DbUtils.GetDateTime(reader, "CreateDateTime"),
                        };
                    }
                    reader.Close();

                    return userProfile;
                }
            }
        }

        public void Add(UserProfile userProfile)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO UserProfile (FirebaseUserId, Name, 
                                                                 Email, CreateDateTime)
                                        OUTPUT INSERTED.ID
                                        VALUES (@FirebaseUserId, @Name, 
                                                @Email, @CreateDateTime)";
                    DbUtils.AddParameter(cmd, "@FirebaseUserId", userProfile.FirebaseUserId);
                    DbUtils.AddParameter(cmd, "@Name", userProfile.Name);
                    DbUtils.AddParameter(cmd, "@Email", userProfile.Email);
                    DbUtils.AddParameter(cmd, "@CreateDateTime", userProfile.CreateDateTime);

                    userProfile.Id = (int)cmd.ExecuteScalar();
                }
            }
        }


        public List<UserProfile> GetAllUsers()
        {
            using (SqlConnection conn = Connection)
            {
                List<UserProfile> users = new List<UserProfile>();
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                SELECT u.id, u.FirebaseUserId, u.Name, u.Email,
                                    u.CreateDateTime
                                    ut.[Name] AS UserTypeName
                                FROM UserProfile
                                ORDER BY DisplayName
                                ";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            UserProfile userProfile = new UserProfile
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                FirebaseUserId = DbUtils.GetString(reader, "firebaseUserId"),
                                Name = DbUtils.GetString(reader, "Name"),
                                Email = DbUtils.GetString(reader, "email"),
                                CreateDateTime = DbUtils.GetDateTime(reader, "CreateDateTime")
                            };
                            users.Add(userProfile);
                        }
                    }
                    return users;
                }
            }
        }
    }
}





/*
public UserProfile GetByFirebaseUserId(string firebaseUserId)
{
    return _context.UserProfile
               .Include(up => up.UserType) 
               .FirstOrDefault(up => up.FirebaseUserId == firebaseUserId);
}

public void Add(UserProfile userProfile)
{
    _context.Add(userProfile);
    _context.SaveChanges();
}
*/

