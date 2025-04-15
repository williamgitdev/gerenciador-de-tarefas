namespace GerenciadorTarefas.Core.Entidades
{
    public class Projeto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public List<Tarefa> Tarefas { get; set; } = new List<Tarefa>();

        public void AdicionarTarefa(Tarefa tarefa)
        {
            if (Tarefas.Count >= 20)
            {
                throw new InvalidOperationException("Limite de 20 tarefas por projeto atingido.");
            }
            Tarefas.Add(tarefa);
        }

        public void RemoverTarefa(Tarefa tarefa)
        {
            Tarefas.Remove(tarefa);
        }
    }
}