using AutoMapper;
using Labs.API._2___Application.Interfaces;
using Labs.API._3___Models.Response;
using Labs.API._4___Infra.Interfaces;
using Labs.API.Config;
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
    }
}
