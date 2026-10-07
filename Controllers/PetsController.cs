using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pet_Shop.Data;
using Pet_Shop.Models;

namespace Pet_Shop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetsController : ControllerBase
    {
        private readonly PetShopContext _context;

        public PetsController(PetShopContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? especie, [FromQuery] string? nome)
        {
            var query = _context.Pets.Include(p => p.Cliente).AsQueryable();

            if (!string.IsNullOrWhiteSpace(especie))
            {
                query = query.Where(p => p.Especie.Contains(especie));
            }

            if (!string.IsNullOrWhiteSpace(nome))
            {
                query = query.Where(p => p.Nome.Contains(nome));
            }

            var pets = await query.ToListAsync();
            return Ok(pets); 
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var pet = await _context.Pets.Include(p => p.Cliente).FirstOrDefaultAsync(p => p.Id == id);

            if (pet == null)
            {
                return NotFound(new { mensagem = "Pet não encontrado." }); 
            }

            return Ok(pet); 
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Pet pet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }

            _context.Pets.Add(pet);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = pet.Id }, pet); 
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Pet pet)
        {
            if (id != pet.Id)
            {
                return BadRequest(new { mensagem = "O ID informado na URL não coincide com o ID do objeto." }); 
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var petExistente = await _context.Pets.FindAsync(id);
            if (petExistente == null)
            {
                return NotFound(new { mensagem = "Pet não encontrado para atualização." }); 
            }

            petExistente.Nome = pet.Nome;
            petExistente.Especie = pet.Especie;
            petExistente.Raca = pet.Raca;
            petExistente.Idade = pet.Idade;
            petExistente.ClienteId = pet.ClienteId;

            await _context.SaveChangesAsync();
            return NoContent(); 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var pet = await _context.Pets.FindAsync(id);
            if (pet == null)
            {
                return NotFound(new { mensagem = "Pet não encontrado para remoção." }); 
            }

            _context.Pets.Remove(pet);
            await _context.SaveChangesAsync();

            return NoContent(); 
        }
    }
}