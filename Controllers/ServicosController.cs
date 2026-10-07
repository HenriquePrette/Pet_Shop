using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pet_Shop.Data;
using Pet_Shop.Models;

namespace Pet_Shop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicosController : ControllerBase
    {
        private readonly PetShopContext _context;

        public ServicosController(PetShopContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var servicos = await _context.Servicos.ToListAsync();
            return Ok(servicos); 
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var servico = await _context.Servicos.FindAsync(id);
            if (servico == null)
            {
                return NotFound(new { mensagem = "Serviço não encontrado." }); 
            }

            return Ok(servico); 
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Servico servico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }

            _context.Servicos.Add(servico);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = servico.Id }, servico); 
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Servico servico)
        {
            if (id != servico.Id)
            {
                return BadRequest(new { mensagem = "O ID da URL não coincide com o objeto enviado." }); 
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var servicoExistente = await _context.Servicos.FindAsync(id);
            if (servicoExistente == null)
            {
                return NotFound(new { mensagem = "Serviço não encontrado para atualização." }); 
            }

            servicoExistente.Nome = servico.Nome;
            servicoExistente.Descricao = servico.Descricao;
            servicoExistente.Preco = servico.Preco;

            await _context.SaveChangesAsync();
            return NoContent(); 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var servico = await _context.Servicos.FindAsync(id);
            if (servico == null)
            {
                return NotFound(new { mensagem = "Serviço não encontrado para remoção." }); 
            }

            _context.Servicos.Remove(servico);
            await _context.SaveChangesAsync();

            return NoContent(); 
        }
    }
}