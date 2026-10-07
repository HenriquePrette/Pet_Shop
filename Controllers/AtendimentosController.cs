using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pet_Shop.Data;
using Pet_Shop.Models;

namespace Pet_Shop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AtendimentosController : ControllerBase
    {
        private readonly PetShopContext _context;

        public AtendimentosController(PetShopContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? status)
        {
            var query = _context.Atendimentos
                .Include(a => a.Pet)
                .Include(a => a.Servico)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(status))
            {
                query = query.Where(a => a.Status.Contains(status));
            }

            var atendimentos = await query.ToListAsync();
            return Ok(atendimentos); 
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var atendimento = await _context.Atendimentos
                .Include(a => a.Pet)
                .Include(a => a.Servico)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (atendimento == null)
            {
                return NotFound(new { mensagem = "Atendimento não encontrado." }); 
            }

            return Ok(atendimento); 
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Atendimento atendimento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }

            var petExiste = await _context.Pets.AnyAsync(p => p.Id == atendimento.PetId);
            if (!petExiste)
            {
                return BadRequest(new { mensagem = "O PetId informado não existe no sistema." });
            }

            var servicoExiste = await _context.Servicos.AnyAsync(s => s.Id == atendimento.ServicoId);
            if (!servicoExiste)
            {
                return BadRequest(new { mensagem = "O ServicoId informado não existe no sistema." });
            }

            _context.Atendimentos.Add(atendimento);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = atendimento.Id }, atendimento); 
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Atendimento atendimento)
        {
            if (id != atendimento.Id)
            {
                return BadRequest(new { mensagem = "O ID da URL não coincide com o objeto enviado." }); 
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var atendimentoExistente = await _context.Atendimentos.FindAsync(id);
            if (atendimentoExistente == null)
            {
                return NotFound(new { mensagem = "Atendimento não encontrado para atualização." }); 
            }

            atendimentoExistente.PetId = atendimento.PetId;
            atendimentoExistente.ServicoId = atendimento.ServicoId;
            atendimentoExistente.Data = atendimento.Data;
            atendimentoExistente.Valor = atendimento.Valor;
            atendimentoExistente.Status = atendimento.Status;

            await _context.SaveChangesAsync();
            return NoContent(); 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var atendimento = await _context.Atendimentos.FindAsync(id);
            if (atendimento == null)
            {
                return NotFound(new { mensagem = "Atendimento não encontrado para remoção." });
            }

            _context.Atendimentos.Remove(atendimento);
            await _context.SaveChangesAsync();

            return NoContent(); 
        }
    }
}