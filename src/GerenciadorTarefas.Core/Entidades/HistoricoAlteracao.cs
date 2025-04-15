public class HistoricoAlteracao
{
    public int Id { get; set; }
    public int TarefaId { get; set; }
    public string Modificacao { get; set; }
    public DateTime DataModificacao { get; set; }
    public string Usuario { get; set; }

    public HistoricoAlteracao(int tarefaId, string modificacao, string usuario)
    {
        TarefaId = tarefaId;
        Modificacao = modificacao;
        DataModificacao = DateTime.Now;
        Usuario = usuario;
    }
}