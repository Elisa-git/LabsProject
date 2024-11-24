using Labs.API.Models;

namespace Labs.API._4___Infra.Interfaces
{
    public interface IProdutoService
    {
        Task<IEnumerable<Produto>> GetProdutos();
    }
}
