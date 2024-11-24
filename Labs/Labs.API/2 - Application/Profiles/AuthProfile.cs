using AutoMapper;
using Labs.API._3___Models.Request;
using Labs.API._3___Models.Response;
using Labs.API._4___Infra.Entidades;
using Labs.API.Models;
using Labs.API.Models.Request;
using Labs.API.Models.Response;
using Microsoft.AspNetCore.Identity.Data;

namespace Labs.API._2___Application.Profiles
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<LoginRequest, LoginUsuario>();
            CreateMap<User, UserResponse>();

            CreateMap<CadastroRequest, PessoaComAcesso>();

            CreateMap<ProdutoRequest, Produto>();
            CreateMap<Produto, ProdutoResponse>();
        }
    }
}
