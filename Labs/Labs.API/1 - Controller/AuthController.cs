using Labs.API._2___Application.Interfaces;
using Labs.API._4___Infra.Entidades;
using Labs.API.Models;
using Labs.API.Models.Request;
using Labs.API.Models.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;

namespace Labs.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<PessoaComAcesso> userManager;
        private readonly IAuthApplication authApplication;

        public AuthController(UserManager<PessoaComAcesso> userManager, IAuthApplication authApplication)
        {
            this.userManager = userManager;
            this.authApplication = authApplication;
        }

        [HttpPost("register")]
        public async Task<IActionResult> CadastrarUsuario(CadastroRequest registerRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await authApplication.CadastrarUsuario(registerRequest);

            if (!user.Succeeded)
                return BadRequest(user.Errors);

            return Ok(new { Message = "User registered successfully" });
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserResponse>> LogarUsuario([FromBody] LoginRequest loginRequest)
        {
            try
            {
                var user = await authApplication.LogarUsuario(loginRequest);
                return Ok(user);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Ocorreu um erro.", Details = ex.Message });
            }
        }


        [HttpGet("confirma-email")]
        public async Task<IActionResult> ConfirmarEmail(string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
                return BadRequest("Dados inválidos.");

            var result = await authApplication.ConfirmarEmailAsync(userId, token);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok("E-mail confirmado com sucesso!");
        }

    }
}
