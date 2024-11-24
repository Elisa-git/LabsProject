using Labs.API._4___Infra.Interfaces;
using Labs.API.Config;
using Labs.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Labs.API._4___Infra
{
    public class ProdutoService : IProdutoService
    {
        private readonly LabsDBContext context;

        public ProdutoService(LabsDBContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Produto>> GetProdutos()
        {
            return await context.Produtos.ToListAsync();
        }
    }
}
