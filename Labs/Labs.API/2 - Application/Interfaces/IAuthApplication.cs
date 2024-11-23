using Labs.API.Models.Request;
using Labs.API.Models.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;

namespace Labs.API._2___Application.Interfaces
{
    public interface IAuthApplication
    {
        Task<UserResponse> LogarUsuario(LoginRequest loginRequest);
        Task<IdentityResult> CadastrarUsuario(CadastroRequest cadastroRequest);
        Task<IdentityResult> ConfirmarEmailAsync(string userId, string token);
    }
}
