using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using GerenciadorTarefas.Aplicacao.Servicos;
using GerenciadorTarefas.Core.Entidades;
using GerenciadorTarefas.Core.Interfaces;

namespace GerenciadorTarefas.Testes.Unidade.ServicosTests
{
    public class TarefaServicoTests
    {
        private readonly ITarefaRepositorio _repositorioFalso;
        private readonly TarefaServico _tarefaServico;

        public TarefaServicoTests()
        {
            _repositorioFalso = new RepositorioFalso();
            _tarefaServico = new TarefaServico(_repositorioFalso);
        }

        [Fact]
        public void AdicionarTarefa_DeveAdicionarTarefaComSucesso()
        {
            var tarefa = new Tarefa
            {
                Titulo = "Nova Tarefa",
                Descricao = "Descrição da nova tarefa",
                DataVencimento = DateTime.Now.AddDays(5),
                Status = StatusTarefa.Pendente,
                Prioridade = PrioridadeTarefa.Media
            };

            _tarefaServico.AdicionarTarefa(tarefa);

            var tarefas = _repositorioFalso.ObterTodasTarefas();
            Assert.Contains(tarefa, tarefas);
        }

        [Fact]
        public void AtualizarTarefa_DeveAtualizarTarefaComSucesso()
        {
            var tarefa = new Tarefa
            {
                Titulo = "Tarefa Existente",
                Descricao = "Descrição da tarefa existente",
                DataVencimento = DateTime.Now.AddDays(3),
                Status = StatusTarefa.Pendente,
                Prioridade = PrioridadeTarefa.Alta
            };

            _tarefaServico.AdicionarTarefa(tarefa);

            tarefa.Status = StatusTarefa.Concluida;
            _tarefaServico.AtualizarTarefa(tarefa);

            var tarefaAtualizada = _repositorioFalso.ObterTarefaPorId(tarefa.Id);
            Assert.Equal(StatusTarefa.Concluida, tarefaAtualizada.Status);
        }

        [Fact]
        public void RemoverTarefa_DeveRemoverTarefaComSucesso()
        {
            var tarefa = new Tarefa
            {
                Titulo = "Tarefa para Remover",
                Descricao = "Descrição da tarefa para remover",
                DataVencimento = DateTime.Now.AddDays(2),
                Status = StatusTarefa.Pendente,
                Prioridade = PrioridadeTarefa.Baixa
            };

            _tarefaServico.AdicionarTarefa(tarefa);
            _tarefaServico.RemoverTarefa(tarefa.Id);

            var tarefas = _repositorioFalso.ObterTodasTarefas();
            Assert.DoesNotContain(tarefa, tarefas);
        }

        [Fact]
        public void AdicionarTarefa_LimiteDeTarefasPorProjeto()
        {
            for (int i = 0; i < 20; i++)
            {
                var tarefa = new Tarefa
                {
                    Titulo = $"Tarefa {i + 1}",
                    Descricao = "Descrição da tarefa",
                    DataVencimento = DateTime.Now.AddDays(1),
                    Status = StatusTarefa.Pendente,
                    Prioridade = PrioridadeTarefa.Media
                };

                _tarefaServico.AdicionarTarefa(tarefa);
            }

            var tarefaExcedente = new Tarefa
            {
                Titulo = "Tarefa Excedente",
                Descricao = "Descrição da tarefa excedente",
                DataVencimento = DateTime.Now.AddDays(1),
                Status = StatusTarefa.Pendente,
                Prioridade = PrioridadeTarefa.Media
            };

            var exception = Assert.Throws<Exception>(() => _tarefaServico.AdicionarTarefa(tarefaExcedente));
            Assert.Equal("Limite de 20 tarefas por projeto excedido.", exception.Message);
        }
    }

    public class RepositorioFalso : ITarefaRepositorio
    {
        private readonly List<Tarefa> _tarefas = new List<Tarefa>();

        public void Adicionar(Tarefa tarefa)
        {
            tarefa.Id = Guid.NewGuid();
            _tarefas.Add(tarefa);
        }

        public void Atualizar(Tarefa tarefa)
        {
            var tarefaExistente = _tarefas.FirstOrDefault(t => t.Id == tarefa.Id);
            if (tarefaExistente != null)
            {
                tarefaExistente.Titulo = tarefa.Titulo;
                tarefaExistente.Descricao = tarefa.Descricao;
                tarefaExistente.DataVencimento = tarefa.DataVencimento;
                tarefaExistente.Status = tarefa.Status;
                tarefaExistente.Prioridade = tarefa.Prioridade;
            }
        }

        public void Remover(Guid id)
        {
            var tarefa = _tarefas.FirstOrDefault(t => t.Id == id);
            if (tarefa != null)
            {
                _tarefas.Remove(tarefa);
            }
        }

        public Tarefa ObterTarefaPorId(Guid id)
        {
            return _tarefas.FirstOrDefault(t => t.Id == id);
        }

        public List<Tarefa> ObterTodasTarefas()
        {
            return _tarefas;
        }
    }
}