using Xunit;
using GerenciadorTarefas.Infraestrutura.Persistencia.Repositorios;
using GerenciadorTarefas.Core.Entidades;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace GerenciadorTarefas.Testes.Unidade.RepositoriosTests
{
    public class TarefaRepositorioTests
    {
        private readonly TarefaRepositorio _repositorio;

        public TarefaRepositorioTests()
        {
            // Inicialização do repositório, normalmente você usaria um contexto de banco de dados em memória para testes
            _repositorio = new TarefaRepositorio(/* Passar o contexto aqui */);
        }

        [Fact]
        public async Task AdicionarTarefa_DeveAdicionarTarefaComSucesso()
        {
            // Arrange
            var tarefa = new Tarefa
            {
                Titulo = "Tarefa de Teste",
                Descricao = "Descrição da tarefa de teste",
                DataVencimento = DateTime.Now.AddDays(5),
                Status = StatusTarefa.Pendente,
                Prioridade = PrioridadeTarefa.Media
            };

            // Act
            await _repositorio.AdicionarTarefa(tarefa);
            var tarefas = await _repositorio.ObterTodasTarefas();

            // Assert
            Assert.Contains(tarefas, t => t.Titulo == tarefa.Titulo);
        }

        [Fact]
        public async Task RemoverTarefa_DeveRemoverTarefaComSucesso()
        {
            // Arrange
            var tarefa = new Tarefa
            {
                Titulo = "Tarefa para Remover",
                Descricao = "Descrição da tarefa a ser removida",
                DataVencimento = DateTime.Now.AddDays(5),
                Status = StatusTarefa.Pendente,
                Prioridade = PrioridadeTarefa.Media
            };

            await _repositorio.AdicionarTarefa(tarefa);
            var tarefasAntes = await _repositorio.ObterTodasTarefas();
            Assert.Contains(tarefasAntes, t => t.Titulo == tarefa.Titulo);

            // Act
            await _repositorio.RemoverTarefa(tarefa.Id);
            var tarefasDepois = await _repositorio.ObterTodasTarefas();

            // Assert
            Assert.DoesNotContain(tarefasDepois, t => t.Titulo == tarefa.Titulo);
        }

        [Fact]
        public async Task AtualizarTarefa_DeveAtualizarTarefaComSucesso()
        {
            // Arrange
            var tarefa = new Tarefa
            {
                Titulo = "Tarefa para Atualizar",
                Descricao = "Descrição da tarefa a ser atualizada",
                DataVencimento = DateTime.Now.AddDays(5),
                Status = StatusTarefa.Pendente,
                Prioridade = PrioridadeTarefa.Media
            };

            await _repositorio.AdicionarTarefa(tarefa);

            // Act
            tarefa.Status = StatusTarefa.Concluida;
            await _repositorio.AtualizarTarefa(tarefa);
            var tarefaAtualizada = await _repositorio.ObterTarefaPorId(tarefa.Id);

            // Assert
            Assert.Equal(StatusTarefa.Concluida, tarefaAtualizada.Status);
        }

        [Fact]
        public async Task ObterTodasTarefas_DeveRetornarListaDeTarefas()
        {
            // Act
            var tarefas = await _repositorio.ObterTodasTarefas();

            // Assert
            Assert.IsType<List<Tarefa>>(tarefas);
        }
    }
}