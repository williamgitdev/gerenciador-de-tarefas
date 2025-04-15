namespace GerenciadorTarefas.Core.Interfaces
{
    public interface IProjetoRepositorio
    {
        IEnumerable<Projeto> ListarProjetos();
        Projeto ObterProjetoPorId(int id);
        void CriarProjeto(Projeto projeto);
        void AtualizarProjeto(Projeto projeto);
        void RemoverProjeto(int id);
    }
}