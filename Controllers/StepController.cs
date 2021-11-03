using CookBook.Models;
using CookBook.Repositories;
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
    public class StepController : ControllerBase
    {
        private readonly IStepRepository _stepRepository;
        private readonly IDishRepository _dishRepository;


        public StepController(IStepRepository stepRepository, IDishRepository dishRepository)
        {
            _stepRepository = stepRepository;
            _dishRepository = dishRepository;

        }
        [HttpGet("{id}")]
        public IActionResult GetStepByDishId(int id)
        {
            var step = _stepRepository.GetStepByDishId(id);
            if (step == null)
            {
                return NotFound();
            }
            return Ok(step);
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            return Ok(_stepRepository.GetAll(id));
        }

        [HttpPost]
        public IActionResult Add(Step step, int id)
        {
            _stepRepository.Add(step);
            return CreatedAtAction("Get", new { id = step.Id }, step);
        }


    }
}
