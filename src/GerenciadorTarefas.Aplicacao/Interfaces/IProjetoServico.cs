namespace GerenciadorTarefas.Aplicacao.Interfaces
{
    public interface IProjetoServico
    {
        IEnumerable<ProjetoDTO> ListarProjetos();
        ProjetoDTO CriarProjeto(ProjetoDTO projeto);
        void RemoverProjeto(int id);
        ProjetoDTO ObterProjetoPorId(int id);
    }
}