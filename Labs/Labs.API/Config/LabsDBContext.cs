using Labs.API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Labs.API.Config
{
    public class LabsDBContext : IdentityDbContext<PessoaComAcesso, PerfilDeAcesso, int>
    {
        public LabsDBContext() { }
        public LabsDBContext(DbContextOptions<LabsDBContext> opt) : base(opt) { }

        public DbSet<Produto> Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            SeedDatabaseInicial(modelBuilder);
        }

        private void SeedDatabaseInicial(ModelBuilder builder)
        {
            builder.Entity<Produto>().HasData
            (
                //new Produto { Id = 1, Nome = "Roda de Carro", Marca = "CAT", Quantidade = 15 },
                //new Produto { Id = 2, Nome = "Roda de Moto", Marca = "CAT", Quantidade = 190 },
                //new Produto { Id = 3, Nome = "Roda de Bicicleta", Marca = "CAT", Quantidade = 130 },
                //new Produto { Id = 4, Nome = "Carro de controle remoto", Marca = "Mattel", Quantidade = 9 },
                //new Produto { Id = 5, Nome = "Boneca Barbie", Marca = "Mattel", Quantidade = 15 },
                //new Produto { Id = 6, Nome = "Pen Drive", Marca = "SanDisk", Quantidade = 1 }

            );
        }
    }
}
