using Labs.API._3___Models.Request;
using Labs.API._3___Models.Response;

namespace Labs.API._2___Application.Interfaces
{
    public interface IProdutoApplication
    {
        Task<IEnumerable<ProdutoResponse>> GetProdutos();
        Task<ProdutoResponse> GetProduto(int id);
        Task PutProduto(int id, ProdutoRequest produto);
        Task<ProdutoResponse> PostProduto(ProdutoRequest produto);
        Task DeleteProduto(int id);
    }
}
