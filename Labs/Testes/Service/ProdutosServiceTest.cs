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
            //context = Substitute.For<LabsDBContext>();
            //logger = Substitute.For<ILogger<ProdutoService>>();

            var options = new DbContextOptionsBuilder<LabsDBContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;

            context = new LabsDBContext(options);
            context.Produtos.AddRange(Builder<Produto>.CreateListOfSize(2).Build());

            sut = new ProdutoService(
                context, logger
            );
        }

        [Collection("Sequential")]
        public class GetProdutosMetodo : ProdutosServiceTest
        {
            [Fact]
            public async Task Dado_ObjetoIntegro_Espero_ListaProdutos()
            {
                //context.Produtos.AddRange(Builder<Produto>.CreateListOfSize(2).Build());
                //await context.SaveChangesAsync();

                var retorno = await sut.GetProdutos();

                retorno.Should().BeEquivalentTo(context.Produtos.ToList());
            }
        }

        [Collection("Sequential")]
        public class GetProdutoMetodo : ProdutosServiceTest
        {
            [Fact]
            public async Task Dado_ObjetoIntegro_Espero_RetornaProduto()
            {
                var produto = Builder<Produto>.CreateNew().Build();
                //context.Produtos.Add(produto);
                //await context.SaveChangesAsync();

                var retorno = await sut.GetProduto(produto.Id);

                retorno.Should().Be(produto);
            }
        }

        [Collection("Sequential")]
        public class PutProdutoMetodo : ProdutosServiceTest
        {
            [Fact]
            public async Task Dado_ObjetoIntegro_Espero_ProdutoAtualizado()
            {
                var produto = Builder<Produto>.CreateNew().Build();
                //context.Produtos.Add(produto);
                //await context.SaveChangesAsync();

                var service = new ProdutoService(context, logger);

                await service.PutProduto(1, produto);

                var produtoAtualizado = context.Produtos.Find(1);
                Assert.Equal(produto.Nome, produtoAtualizado.Nome);
            }

            [Fact]
            public async Task Dado_IdsDiferentes_Espero_ArgumentException()
            {
                var produto = Builder<Produto>.CreateNew()
                    .With(x => x.Id, 1)
                    .Build();

                await Assert.ThrowsAsync<ArgumentException>(async () =>
                {
                    await sut.PutProduto(2, produto);
                });
            }

        }

        [Collection("Sequential")]
        public class PostProdutoMetodo : ProdutosServiceTest
        {
            [Fact]
            public async Task Dado_ProdutoValido_Espero_SalvarERetornarProduto()
            {
                var produto = Builder<Produto>.CreateNew()
                    .With(x => x.Id, 1)
                    .With(x => x.Nome, "Produto Teste")
                    .Build();

                var resultado = await sut.PostProduto(produto);

                Assert.Equal(produto.Id, resultado.Id);
                Assert.Equal(produto.Nome, resultado.Nome);

                var produtoSalvo = await context.Produtos.FindAsync(1);
                Assert.NotNull(produtoSalvo);
                Assert.Equal("Produto Teste", produtoSalvo.Nome);
            }
        }

        [Collection("Sequential")]
        public class DeleteProdutoMetodo : ProdutosServiceTest
        {

            [Fact]
            public async Task Dado_ProdutoExistente_Espero_RemoverProduto()
            {
                var produto = Builder<Produto>.CreateNew()
                    .With(x => x.Id, 1)
                    .With(x => x.Nome, "Produto Teste")
                    .Build();
                //context.Produtos.Add(produto);
                //await context.SaveChangesAsync();

                await sut.DeleteProduto(1);

                var produtoDeletado = await context.Produtos.FindAsync(1);
                Assert.Null(produtoDeletado);
            }

        }
    }
}