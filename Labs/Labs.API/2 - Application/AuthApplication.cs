using AutoMapper;
using Labs.API._2___Application.Interfaces;
using Labs.API._4___Infra.Entidades;
using Labs.API._4___Infra.Interfaces;
using Labs.API.Models;
using Labs.API.Models.Request;
using Labs.API.Models.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;

namespace Labs.API._2___Application
{
    public class AuthApplication : IAuthApplication
    {
        private readonly IMapper mapper;
        private readonly IAuthService authService;

        public AuthApplication(IMapper mapper, IAuthService authService)
        {
            this.mapper = mapper;
            this.authService = authService;
        }

        public async Task<UserResponse> LogarUsuario(LoginRequest loginRequest)
        {
            var login = await authService.LogarUsuario(mapper.Map<LoginUsuario>(loginRequest));
            return mapper.Map<UserResponse>(login);
        }

        public async Task<IdentityResult> CadastrarUsuario(CadastroRequest cadastroRequest)
        {
            return await authService.CadastrarUsuario(mapper.Map<PessoaComAcesso>(cadastroRequest), cadastroRequest.Password);
        }

        public async Task<IdentityResult> ConfirmarEmailAsync(string userId, string token)
        {
            return await authService.ConfirmarEmailAsync(userId, token);
        }

    }
}
