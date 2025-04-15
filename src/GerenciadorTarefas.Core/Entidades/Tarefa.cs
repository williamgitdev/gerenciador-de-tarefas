namespace GerenciadorTarefas.Core.Entidades
{
    public class Tarefa
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataVencimento { get; set; }
        public StatusTarefa Status { get; set; }
        public PrioridadeTarefa Prioridade { get; set; }
        public List<Comentario> Comentarios { get; set; } = new List<Comentario>();
        public List<HistoricoAlteracao> HistoricoAlteracoes { get; set; } = new List<HistoricoAlteracao>();
    }
}