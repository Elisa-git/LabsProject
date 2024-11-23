﻿using Labs.API._4___Infra.Entidades;
using Labs.API._4___Infra.Interfaces;
using Labs.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Labs.API._4___Infra
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<PessoaComAcesso> userManager;
        private readonly IEmailSender emailSender;
        private readonly IUrlHelperFactory urlHelperFactory;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ILogger<AuthService> logger;
        public AuthService(UserManager<PessoaComAcesso> userManager, IEmailSender emailSender, IUrlHelperFactory urlHelperFactory, IHttpContextAccessor httpContextAccessor, ILogger<AuthService> logger)
        {
            this.userManager = userManager;
            this.emailSender = emailSender;
            this.urlHelperFactory = urlHelperFactory;
            this.httpContextAccessor = httpContextAccessor;
            this.logger = logger;
        }

        public async Task<User> LogarUsuario(LoginUsuario login)
        {
            var user = await userManager.FindByEmailAsync(login.Email);

            if (user == null)
                throw new UnauthorizedAccessException("Email não cadastrado no sistema.");

            if (!await userManager.CheckPasswordAsync(user, login.Password))
                throw new UnauthorizedAccessException("Usuário ou senha inválidos.");

            if (!user.EmailConfirmed)
                throw new UnauthorizedAccessException("Você precisa confirmar sua conta antes e fazer login. Verifique seu email.");

            var userLogado = new User();
            userLogado.SetEmail(login.Email);
            userLogado.SetNome(user.Nome);

            return userLogado;
        }

        public async Task<IdentityResult> CadastrarUsuario(PessoaComAcesso user, string senha)
        {
            user.UserName = user.Email;
            var retorno = await userManager.CreateAsync(user, senha);

            if (!retorno.Succeeded)
            {
                return retorno;
            }

            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

            var httpContext = httpContextAccessor.HttpContext;
            if (httpContext == null)
                throw new InvalidOperationException("HttpContext is null.");

            var urlHelper = urlHelperFactory.GetUrlHelper(new ActionContext
            {
                HttpContext = httpContext,
                RouteData = httpContext.GetRouteData(),
                ActionDescriptor = new Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor()
            });

            var confirmationLink = urlHelper.Action(
                "ConfirmarEmail",
                "auth", // Nome do controlador
                new { userId = user.Id, token },
                httpContext.Request.Scheme);

            try
            {
                await emailSender.SendEmailAsync(user.Email, "Confirme seu e-mail",
                    $"Por favor, confirme sua conta clicando no link: <a href='{confirmationLink}'>Confirmar</a>");
                logger.LogInformation($"E-mail enviado com sucesso para {user.Email}");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Erro ao enviar e-mail para {user.Email}");
            }


            return IdentityResult.Success;
        }

        public async Task<IdentityResult> ConfirmarEmailAsync(string userId, string token)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "Usuário não encontrado." });

            var retorno = await userManager.ConfirmEmailAsync(user, token);

            return retorno;
        }
    }
}
