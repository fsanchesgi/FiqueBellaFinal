using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FiqueBellaFinal.Data;
using FiqueBellaFinal.Models;

namespace FiqueBellaFinal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /api/produtos
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var produtos = await _context.Produtos.ToListAsync();
            return Ok(produtos);
        }

        // POST: /api/produtos
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Produto produto)
        {
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = produto.Id }, produto);
        }
    }
}
