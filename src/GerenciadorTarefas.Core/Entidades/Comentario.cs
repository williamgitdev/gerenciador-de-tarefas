namespace GerenciadorTarefas.Core.Entidades
{
    public class Comentario
    {
        public int Id { get; set; }
        public int TarefaId { get; set; }
        public string Texto { get; set; }
        public DateTime DataCriacao { get; set; }
        public string Usuario { get; set; }

        public Comentario()
        {
            DataCriacao = DateTime.Now;
        }
    }
}