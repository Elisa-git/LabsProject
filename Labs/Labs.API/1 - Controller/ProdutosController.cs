using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Labs.API.Config;
using Labs.API.Models;
using Microsoft.AspNetCore.Authorization;

namespace Labs.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly LabsDBContext _context;

        public ProdutosController(LabsDBContext context)
        {
            _context = context;
        }

        // GET: api/Produtos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos()
        {
            return await _context.Produtos.ToListAsync();
        }

        // GET: api/Produtos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> GetProduto(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);

            if (produto == null)
            {
                return NotFound();
            }

            return produto;
        }

        // PUT: api/Produtos/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutProduto(int id, Produto produto)
        {
            if (id != produto.Id)
            {
                return BadRequest();
            }

            _context.Entry(produto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdutoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Produtos
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Produto>> PostProduto(Produto produto)
        {
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduto", new { id = produto.Id }, produto);
        }

        // DELETE: api/Produtos/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteProduto(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }

            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProdutoExists(long id)
        {
            return _context.Produtos.Any(e => e.Id == id);
        }
    }
}
