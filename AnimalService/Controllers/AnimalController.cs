using AnimalService.Contracts;
using AnimalService.Models;
using Microsoft.AspNetCore.Mvc;

namespace AnimalService.Controllers
{
    [Route("api/animal")]
    [ApiController]
    public class AnimalController : ControllerBase
    {
        private readonly IAnimalRepository _animalRepository;

        public AnimalController(IAnimalRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var animals = await _animalRepository.GetAnimals();
                return Ok(animals);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var animal = await _animalRepository.GetAnimal(id);
                if (animal == null)
                    return NotFound();
                return Ok(animal);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("Filter")]
        public async Task<IActionResult> GetFiltered([FromQuery(Name = "animalId")] Guid? id=null, [FromQuery(Name = "name")] string? name=null, [FromQuery(Name = "sex")] bool? sex=null, [FromQuery(Name = "status")] bool? status=null)
        {
            if (id == null && name == null && sex == null && status == null)
            {
                return BadRequest();
            }
            try
            {
                var animals = await _animalRepository.GetAnimalsFiltered(id, name, sex, status);
                return Ok(animals);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("Order")]
        public async Task<IActionResult> Order()
        {
            try
            {
                var animals = await _animalRepository.GetAnimalsFiltered(id, name, sex, status);
                return Ok(animals);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Animal value)
        {
            try
            {
                var animal = await _animalRepository.CreateAnimal(value);
                return CreatedAtRoute("AnimalById", new { id = animal.AnimalId }, animal);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Animal value)
        {
            try
            {
                var currentAnimal = await _animalRepository.GetAnimal(id);
                if (currentAnimal == null)
                    return NotFound();
                await _animalRepository.UpdateAnimal(id, value);
                return NoContent();
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var currentAnimal = await _animalRepository.GetAnimal(id);
                if (currentAnimal == null)
                    return NotFound();
                await _animalRepository.DeleteAnimal(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
    }
}
