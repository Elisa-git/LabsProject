using Labs.API._4___Infra.Interfaces;
using Labs.API.Config;
using Labs.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Labs.API._4___Infra
{
    public class ProdutoService : IProdutoService
    {
        private readonly LabsDBContext context;
        private readonly ILogger<ProdutoService> logger;

        public ProdutoService(LabsDBContext context, ILogger<ProdutoService> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public async Task<IEnumerable<Produto>> GetProdutos()
        {
            return await context.Produtos.ToListAsync();
        }

        public async Task<Produto> GetProduto(int id)
        {
            return await context.Produtos.FindAsync(id);
        }

        public async Task PutProduto(int id, Produto produto)
        {
            if (id != produto.Id)
            {
                throw new ArgumentException("O ID do produto não corresponde ao parâmetro fornecido.");
            }

            context.Entry(produto).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException except)
            {
                if (!ProdutoExists(id))
                {
                    logger.LogError("Erro ao tentar atualizar produto: Produto nao encontrado - {0}", except);
                    throw new KeyNotFoundException("Produto não encontrado.");
                }
                else
                {
                    logger.LogError("Erro ao tentar atualizar produto: {0}", except);
                    throw;
                }
            }
        }

        private bool ProdutoExists(long id)
        {
            return context.Produtos.Any(e => e.Id == id);
        }

        public async Task<Produto> PostProduto(Produto produto)
        {
            try
            {
                context.Produtos.Add(produto);
                await context.SaveChangesAsync();

                return produto;

            } catch (Exception except)
            {
                logger.LogError("Erro ao tentar cadastrar produto: {0}", except);
                throw new Exception("Erro ao salvar produto", except);
            }
        }

        public async Task DeleteProduto(int id)
        {
            var produto = await context.Produtos.FindAsync(id);
        
            if (produto == null)
            {
                logger.LogError("Erro ao tentar deletar produto: Produto e Id {0} nao encontrado", id);
                throw new KeyNotFoundException("Produto não encontrado.");
            }

            context.Produtos.Remove(produto);
            await context.SaveChangesAsync();
        }


    }
}
