using AutoMapper;
using Labs.API._2___Application.Interfaces;
using Labs.API._3___Models.Request;
using Labs.API._3___Models.Response;
using Labs.API._4___Infra.Interfaces;
using Labs.API.Config;
using Labs.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Labs.API._2___Application
{
    public class ProdutoApplication : IProdutoApplication
    {
        private readonly IProdutoService produtoService;
        private readonly IMapper mapper;

        public ProdutoApplication(IProdutoService produtoService, IMapper mapper)
        {
            this.produtoService = produtoService;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ProdutoResponse>> GetProdutos()
        {
            var resultado = mapper.Map<IEnumerable<ProdutoResponse>>(await produtoService.GetProdutos());
            return resultado;
        }

        public async Task<ProdutoResponse> GetProduto(int id)
        {
            var resultado = mapper.Map<ProdutoResponse>(await produtoService.GetProduto(id));
            return resultado;
        }

        public async Task PutProduto(int id, ProdutoRequest produto)
        {
            var produtoMapeado = mapper.Map<Produto>(produto);
            await produtoService.PutProduto(id, produtoMapeado);
        }

        public async Task<ProdutoResponse> PostProduto(ProdutoRequest produto)
        {
            var produtoMapeado = mapper.Map<Produto>(produto);
            return mapper.Map<ProdutoResponse>(await produtoService.PostProduto(produtoMapeado));
        }

        public async Task DeleteProduto(int id)
        {
            await produtoService.DeleteProduto(id);
        }
    }
}
