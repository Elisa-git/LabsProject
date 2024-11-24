using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Labs.API.Config;
using Labs.API.Models;
using Microsoft.AspNetCore.Authorization;
using Labs.API._2___Application.Interfaces;
using Labs.API._3___Models.Response;
using Labs.API._3___Models.Request;

namespace Labs.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProdutosController : ControllerBase
    {
        private readonly LabsDBContext _context;
        private readonly IProdutoApplication produtoApplication;

        public ProdutosController(LabsDBContext context, IProdutoApplication produtoApplication)
        {
            _context = context;
            this.produtoApplication = produtoApplication;
        }

        // GET: api/Produtos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoResponse>>> GetProdutos()
        {
            var result = await produtoApplication.GetProdutos();

            return Ok(result);
        }

        // GET: api/Produtos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> GetProduto(int id)
        {
            var produto = await produtoApplication.GetProduto(id);

            if (produto == null)
            {
                return NotFound();
            }

            return Ok(produto);
        }

        // PUT: api/Produtos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduto(int id, ProdutoRequest produto)
        {
            try
            {
                await produtoApplication.PutProduto(id, produto);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // POST: api/Produtos
        [HttpPost]
        public async Task<ActionResult<ProdutoResponse>> PostProduto(ProdutoRequest produto)
        {
            var retorno = produtoApplication.PostProduto(produto);
            return CreatedAtAction("GetProduto", new { id = retorno.Id }, retorno);
        }

        // DELETE: api/Produtos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduto(int id)
        {
            await produtoApplication.DeleteProduto(id);
            return NoContent();
        }

    }
}
