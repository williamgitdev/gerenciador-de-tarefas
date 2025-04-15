using GerenciadorTarefas.Core.Entidades;
using GerenciadorTarefas.Core.Interfaces;
using GerenciadorTarefas.Aplicacao.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace GerenciadorTarefas.Aplicacao.Servicos
{
    public class ProjetoServico : IProjetoServico
    {
        private readonly IProjetoRepositorio _projetoRepositorio;

        public ProjetoServico(IProjetoRepositorio projetoRepositorio)
        {
            _projetoRepositorio = projetoRepositorio;
        }

        public IEnumerable<Projeto> ListarProjetos()
        {
            return _projetoRepositorio.ObterTodos();
        }

        public Projeto CriarProjeto(Projeto projeto)
        {
            return _projetoRepositorio.Adicionar(projeto);
        }

        public void RemoverProjeto(int id)
        {
            var projeto = _projetoRepositorio.ObterPorId(id);
            if (projeto == null)
            {
                throw new KeyNotFoundException("Projeto não encontrado.");
            }

            if (projeto.Tarefas.Any(t => t.Status == StatusTarefa.Pendente))
            {
                throw new InvalidOperationException("Não é possível remover o projeto com tarefas pendentes.");
            }

            _projetoRepositorio.Remover(projeto);
        }
    }
}