using CookBook.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookBook.Controllers
{
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
        public IActionResult Get()
        {
            return Ok(_stepRepository.GetAll(1026));
        }
    }
}
