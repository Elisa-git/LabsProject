using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FizzWare.NBuilder;
using FluentAssertions;
using Labs.API._4___Infra.Entidades;

namespace Testes.Entidade
{
    public class UserTeste
    {
        private readonly User sut;
        const string NOME = "Teste";

        public UserTeste()
        {
            this.sut = Builder<User>.CreateNew().Build();
        }

        public class Construtor : UserTeste
        {
            [Fact]
            public void Quando_SetNomeForChamado_Espero_NomeValido()
            {
                sut.SetNome(NOME);
                sut.nome.Should().Be(NOME);
            }
            
            [Fact]
            public void Quando_SetEmailForChamado_Espero_EmailValido()
            {
                sut.SetEmail(NOME);
                sut.email.Should().Be(NOME);
            }

            [Fact]
            public void Quando_SetTokenForChamado_Espero_TokenValido()
            {
                sut.SetToken(NOME);
                sut.token.Should().Be(NOME);
            }
        }
    }
}
