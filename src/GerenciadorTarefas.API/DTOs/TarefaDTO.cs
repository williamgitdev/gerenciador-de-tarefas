namespace GerenciadorTarefas.API.DTOs
{
    public class TarefaDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataVencimento { get; set; }
        public string Status { get; set; }
        public string Prioridade { get; set; }
        public List<string> Comentarios { get; set; }

        public TarefaDTO()
        {
            Comentarios = new List<string>();
        }
    }
}