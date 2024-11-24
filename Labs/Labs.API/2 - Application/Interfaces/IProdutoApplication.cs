using Labs.API._3___Models.Response;

namespace Labs.API._2___Application.Interfaces
{
    public interface IProdutoApplication
    {
        Task<IEnumerable<ProdutoResponse>> GetProdutos();
    }
}
