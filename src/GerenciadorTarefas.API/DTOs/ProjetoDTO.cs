namespace GerenciadorTarefas.API.DTOs
{
    public class ProjetoDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public List<TarefaDTO> Tarefas { get; set; }
    }
}