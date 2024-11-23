using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace Labs.API._4___Infra.Entidades
{
    public class LoginUsuario
    {
        public LoginUsuario() { }

        public virtual string Email { get; private set; }
        public virtual string Password { get; private set; }

        public virtual void SetEmail(string email)
        {
            Email = email;
        }

        public virtual void SetPassword(string password)
        {
            Password = password;
        }
    }
}
