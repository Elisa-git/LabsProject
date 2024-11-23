using Microsoft.AspNetCore.Identity;

namespace Labs.API.Models
{
    public class PessoaComAcesso : IdentityUser<int>
    {
        public string Nome { get; set; }
    }
}
