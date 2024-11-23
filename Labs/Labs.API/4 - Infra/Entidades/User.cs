using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace Labs.API._4___Infra.Entidades
{
    public class User
    {
        public virtual string nome { get; private set; }
        public virtual string email { get; private set; }

        public virtual void SetNome(string nome)
        {
            this.nome = nome;
        }

        public virtual void SetEmail(string email)
        {
            this.email = email;
        }
    }
}
