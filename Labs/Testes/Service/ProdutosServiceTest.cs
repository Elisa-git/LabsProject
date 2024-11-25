using FizzWare.NBuilder;
using FluentAssertions;
using Labs.API._4___Infra;
using Labs.API.Config;
using Labs.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Testes.Service
{
    public class ProdutosServiceTest
    {
        private readonly ProdutoService sut;
        private readonly LabsDBContext context;
        private readonly ILogger<ProdutoService> logger;

        public ProdutosServiceTest()
        {
            context = Substitute.For<LabsDBContext>();
            logger = Substitute.For<ILogger<ProdutoService>>();
            sut = new ProdutoService(
                context, logger
            );
        }

        [Collection("Sequential")]
        public class GetProdutosMetodo : ProdutosServiceTest
        {
            [Fact]
            public void Dado_ObjetoIntegro_Espero_ListaProdutos()
            {
                var produto = Builder<Produto>.CreateNew().Build();
                context.Produtos.ToListAsync().Returns(new List<Produto>() { produto });
                var retorno = sut.GetProdutos();

                retorno.Should().BeEquivalentTo(new List<Produto>() { produto });
            }
        }
    }
}