using Labs.API._4___Infra.Entidades;
using Labs.API.Models;
using Microsoft.AspNetCore.Identity;

namespace Labs.API._4___Infra.Interfaces
{
    public interface IAuthService
    {
        Task<User> LogarUsuario(LoginUsuario login);
        Task<IdentityResult> CadastrarUsuario(PessoaComAcesso user, string senha);
        Task<IdentityResult> ConfirmarEmailAsync(string userId, string token);
    }
}
