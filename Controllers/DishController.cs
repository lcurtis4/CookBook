using CookBook.Models;
using CookBook.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CookBook.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly IDishRepository _dishRepository;     
        private readonly IUserProfileRepository _userProfileRepository;

        public DishController(IDishRepository dishRepository, IUserProfileRepository userProfileRepository)
        {
            _dishRepository = dishRepository;
            _userProfileRepository = userProfileRepository;
        }

        [HttpPost]
        public IActionResult Add(Dish dish)
        {
            var currentUser = GetCurrentUserProfileId();

            dish.UserProfileId = currentUser.Id;
            _dishRepository.Add(dish);
            return CreatedAtAction("Get", new { id = dish.Id }, dish);

        }

        private UserProfile GetCurrentUserProfileId()
        {
            var firebaseUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return _userProfileRepository.GetByFirebaseUserId(firebaseUserId);
        }
    }
}
