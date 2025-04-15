namespace GerenciadorTarefas.Core.Interfaces
{
    public interface ITarefaRepositorio
    {
        Task<List<Tarefa>> ListarTarefasPorProjetoAsync(int projetoId);
        Task<Tarefa> ObterTarefaPorIdAsync(int tarefaId);
        Task CriarTarefaAsync(Tarefa tarefa);
        Task AtualizarTarefaAsync(Tarefa tarefa);
        Task RemoverTarefaAsync(int tarefaId);
    }
}