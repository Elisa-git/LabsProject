using Labs.API._3___Models.Response;
using Labs.API.Models;

namespace Labs.API._4___Infra.Interfaces
{
    public interface IProdutoService
    {
        Task<IEnumerable<Produto>> GetProdutos();
        Task<Produto> GetProduto(int id);
        Task PutProduto(int id, Produto produto);
        async Task<Produto> PostProduto(Produto produto);
        async Task DeleteProduto(int id);

    }
}
