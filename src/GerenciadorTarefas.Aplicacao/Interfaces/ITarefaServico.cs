namespace GerenciadorTarefas.Aplicacao.Interfaces
{
    public interface ITarefaServico
    {
        IEnumerable<TarefaDTO> ListarTarefasPorProjeto(int projetoId);
        TarefaDTO CriarTarefa(TarefaDTO tarefaDTO);
        TarefaDTO AtualizarTarefa(int tarefaId, TarefaDTO tarefaDTO);
        void RemoverTarefa(int tarefaId);
    }
}