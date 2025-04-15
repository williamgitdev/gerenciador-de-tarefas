using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GerenciadorTarefas.Core.Entidades;

namespace GerenciadorTarefas.Infraestrutura.Persistencia.Configuracoes
{
    public class TarefaConfiguracao : IEntityTypeConfiguration<Tarefa>
    {
        public void Configure(EntityTypeBuilder<Tarefa> builder)
        {
            builder.ToTable("Tarefas");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Titulo)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.Descricao)
                .HasMaxLength(500);

            builder.Property(t => t.DataVencimento)
                .IsRequired();

            builder.Property(t => t.Status)
                .IsRequired();

            builder.Property(t => t.Prioridade)
                .IsRequired();

            builder.HasMany(t => t.Comentarios)
                .WithOne()
                .HasForeignKey(c => c.TarefaId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(t => t.HistoricosAlteracao)
                .WithOne()
                .HasForeignKey(h => h.TarefaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}