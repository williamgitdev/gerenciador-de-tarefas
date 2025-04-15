using Microsoft.EntityFrameworkCore;
using GerenciadorTarefas.Core.Entidades;

namespace GerenciadorTarefas.Infraestrutura.Persistencia.Contexto
{
    public class AppDbContexto : DbContext
    {
        public AppDbContexto(DbContextOptions<AppDbContexto> options) : base(options)
        {
        }

        public DbSet<Projeto> Projetos { get; set; }
        public DbSet<Tarefa> Tarefas { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<HistoricoAlteracao> HistoricosAlteracao { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContexto).Assembly);
        }
    }
}