using FizzWare.NBuilder;
using FluentAssertions;
using Labs.API.Models;

namespace Testes.Entidade
{
    public class ProdutoTeste
    {
        private readonly Produto sut;
        private readonly RandomGenerator randomGenerator = new RandomGenerator();
        const string NOME = "Teste";

        public ProdutoTeste()
        {
            this.sut = Builder<Produto>.CreateNew().Build();
        }

        public class Construtor : ProdutoTeste
        {
            [Fact]
            public void Quando_SetIdForChamado_Espero_IdValido()
            {
                var id = randomGenerator.Int();
                sut.SetId(id);
                sut.Id.Should().Be(id);
            }

            [Fact]
            public void Quando_SetNomeForChamado_Espero_NomeValido()
            {
                sut.SetNome(NOME);
                sut.Nome.Should().Be(NOME);
            }

            [Fact]
            public void Quando_SetMarcaForChamado_Espero_MarcaValida()
            {
                sut.SetMarca(NOME);
                sut.Marca.Should().Be(NOME);
            }

            [Fact]
            public void Quando_SetQuantidadeForChamado_Espero_QuantidadeValida()
            {
                var id = randomGenerator.Int();
                sut.SetQuantidade(id);
                sut.Quantidade.Should().Be(id);
            }
        }
    }
}
