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
                new Produto(1, "Roda de Carro", "CAT", 45),
                new Produto(2, "Roda de Moto", "CAT", 515),
                new Produto(3, "Roda de Bicicleta", "CAT", 1345),
                new Produto(4, "Carro de controle remoto", "Mattel", 15),
                new Produto(5, "Boneca Barbie", "Mattel", 215),
                new Produto(6, "Pen Drive", "SanDisk", 1545)

            );
        }
    }
}
