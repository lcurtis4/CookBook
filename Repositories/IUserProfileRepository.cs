using CookBook.Models;
using System.Collections.Generic;

namespace CookBook.Repositories
{
    public interface IUserProfileRepository
    {
        void Add(UserProfile userProfile);
        List<UserProfile> GetAllUsers();
        UserProfile GetByFirebaseUserId(string firebaseUserId);
    }
}