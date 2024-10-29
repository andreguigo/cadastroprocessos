using CadProcessosApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CadProcessosApp.Data
{
    public class ProcessoDbContext : DbContext
    {
        public ProcessoDbContext(DbContextOptions<ProcessoDbContext> options) : base(options) { }

        public DbSet<Processo> Processos { get; private set; } 
    }
}