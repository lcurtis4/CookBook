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

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_dishRepository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var dish = _dishRepository.GetByDishId(id);
            if (dish == null)
            {
                return NotFound();
            }
            return Ok(dish);
        }

        [HttpPost]
        public IActionResult Add(Dish dish)
        {
            var currentUser = GetCurrentUserProfileId();

            dish.UserProfileId = currentUser.Id;
            _dishRepository.Add(dish);
            return CreatedAtAction("Get", new { id = dish.Id }, dish);

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _dishRepository.Delete(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Dish dish)
        {
            var currentUser = GetCurrentUserProfileId();
            if (id != dish.Id)
            {
                return BadRequest();
            }
            dish.UserProfileId = currentUser.Id;
            dish.CreateDateTime = DateTime.Now;
            _dishRepository.Update(dish);
            return NoContent();
        }



        private UserProfile GetCurrentUserProfileId()
        {
            var firebaseUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return _userProfileRepository.GetByFirebaseUserId(firebaseUserId);
        }
    }
}
