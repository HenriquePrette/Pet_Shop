using Microsoft.AspNetCore.Mvc;
using Pet_Shop.Models;

namespace Pet_Shop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetsController : ControllerBase
    {
        private static readonly List<Pet> Pets = new List<Pet>
        {
            new Pet { Id = 1, Nome = "Zeus", Especie = "Cachorro", Raca = "Rottweiler", Idade = 1, ClienteId = 1 },
            new Pet { Id = 2, Nome = "Thor", Especie = "Cachorro", Raca = "Golden Retriever", Idade = 2, ClienteId = 1 },
            new Pet { Id = 3, Nome = "Miau", Especie = "Gato", Raca = "Siamês", Idade = 2, ClienteId = 2 }
        };

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(Pets);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var pet = Pets.FirstOrDefault(p => p.Id == id);
            if (pet == null)
            {
                return NotFound();
            }
            return Ok(pet);
        }
    }
}
