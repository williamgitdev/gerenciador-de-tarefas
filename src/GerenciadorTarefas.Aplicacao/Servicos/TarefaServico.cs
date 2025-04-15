using GerenciadorTarefas.Core.Entidades;
using GerenciadorTarefas.Core.Interfaces;
using GerenciadorTarefas.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GerenciadorTarefas.Aplicacao.Servicos
{
    public class TarefaServico : ITarefaServico
    {
        private readonly ITarefaRepositorio _tarefaRepositorio;
        private readonly IProjetoRepositorio _projetoRepositorio;

        public TarefaServico(ITarefaRepositorio tarefaRepositorio, IProjetoRepositorio projetoRepositorio)
        {
            _tarefaRepositorio = tarefaRepositorio;
            _projetoRepositorio = projetoRepositorio;
        }

        public IEnumerable<Tarefa> ListarTarefasPorProjeto(Guid projetoId)
        {
            return _tarefaRepositorio.ObterTarefasPorProjeto(projetoId);
        }

        public Tarefa CriarTarefa(Guid projetoId, Tarefa tarefa)
        {
            var projeto = _projetoRepositorio.ObterPorId(projetoId);
            if (projeto == null)
            {
                throw new Exception("Projeto não encontrado.");
            }

            if (projeto.Tarefas.Count >= 20)
            {
                throw new Exception("Limite de 20 tarefas por projeto atingido.");
            }

            tarefa.ProjetoId = projetoId;
            tarefa.DataCriacao = DateTime.Now;
            _tarefaRepositorio.Adicionar(tarefa);
            return tarefa;
        }

        public Tarefa AtualizarTarefa(Guid tarefaId, Tarefa tarefaAtualizada)
        {
            var tarefa = _tarefaRepositorio.ObterPorId(tarefaId);
            if (tarefa == null)
            {
                throw new Exception("Tarefa não encontrada.");
            }

            tarefa.Titulo = tarefaAtualizada.Titulo;
            tarefa.Descricao = tarefaAtualizada.Descricao;
            tarefa.DataVencimento = tarefaAtualizada.DataVencimento;
            tarefa.Status = tarefaAtualizada.Status;

            _tarefaRepositorio.Atualizar(tarefa);
            return tarefa;
        }

        public void RemoverTarefa(Guid tarefaId)
        {
            var tarefa = _tarefaRepositorio.ObterPorId(tarefaId);
            if (tarefa == null)
            {
                throw new Exception("Tarefa não encontrada.");
            }

            _tarefaRepositorio.Remover(tarefa);
        }

        public void AdicionarComentario(Guid tarefaId, string comentario)
        {
            var tarefa = _tarefaRepositorio.ObterPorId(tarefaId);
            if (tarefa == null)
            {
                throw new Exception("Tarefa não encontrada.");
            }

            tarefa.Comentarios.Add(new Comentario { Texto = comentario, DataCriacao = DateTime.Now });
            _tarefaRepositorio.Atualizar(tarefa);
        }
    }
}