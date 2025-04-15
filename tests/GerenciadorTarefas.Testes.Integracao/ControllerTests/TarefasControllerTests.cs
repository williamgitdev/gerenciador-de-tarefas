using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using GerenciadorTarefas.API.Controllers;
using GerenciadorTarefas.Aplicacao.Interfaces;
using GerenciadorTarefas.API.DTOs;
using GerenciadorTarefas.Core.Entidades;

namespace GerenciadorTarefas.Testes.Integracao.ControllerTests
{
    [TestFixture]
    public class TarefasControllerTests
    {
        private TarefasController _tarefasController;
        private Mock<ITarefaServico> _tarefaServicoMock;

        [SetUp]
        public void Setup()
        {
            _tarefaServicoMock = new Mock<ITarefaServico>();
            _tarefasController = new TarefasController(_tarefaServicoMock.Object);
        }

        [Test]
        public async Task ObterTarefas_ProjetoExistente_RetornaListaDeTarefas()
        {
            // Arrange
            var projetoId = 1;
            var tarefas = new List<TarefaDTO>
            {
                new TarefaDTO { Id = 1, Titulo = "Tarefa 1", Descricao = "Descrição 1", Status = "Pendente" },
                new TarefaDTO { Id = 2, Titulo = "Tarefa 2", Descricao = "Descrição 2", Status = "Concluída" }
            };

            _tarefaServicoMock.Setup(s => s.ObterTarefasPorProjetoIdAsync(projetoId)).ReturnsAsync(tarefas);

            // Act
            var resultado = await _tarefasController.ObterTarefas(projetoId);

            // Assert
            var resultadoOk = resultado as OkObjectResult;
            Assert.IsNotNull(resultadoOk);
            Assert.AreEqual(StatusCodes.Status200OK, resultadoOk.StatusCode);
            Assert.AreEqual(tarefas, resultadoOk.Value);
        }

        [Test]
        public async Task CriarTarefa_TarefaValida_RetornaTarefaCriada()
        {
            // Arrange
            var novaTarefa = new TarefaDTO { Titulo = "Nova Tarefa", Descricao = "Descrição da nova tarefa", ProjetoId = 1 };
            _tarefaServicoMock.Setup(s => s.CriarTarefaAsync(novaTarefa)).ReturnsAsync(novaTarefa);

            // Act
            var resultado = await _tarefasController.CriarTarefa(novaTarefa);

            // Assert
            var resultadoCreated = resultado as CreatedAtActionResult;
            Assert.IsNotNull(resultadoCreated);
            Assert.AreEqual(StatusCodes.Status201Created, resultadoCreated.StatusCode);
            Assert.AreEqual(novaTarefa, resultadoCreated.Value);
        }

        [Test]
        public async Task AtualizarTarefa_TarefaExistente_RetornaTarefaAtualizada()
        {
            // Arrange
            var tarefaAtualizada = new TarefaDTO { Id = 1, Titulo = "Tarefa Atualizada", Descricao = "Descrição atualizada", Status = "EmAndamento" };
            _tarefaServicoMock.Setup(s => s.AtualizarTarefaAsync(tarefaAtualizada)).ReturnsAsync(tarefaAtualizada);

            // Act
            var resultado = await _tarefasController.AtualizarTarefa(tarefaAtualizada.Id, tarefaAtualizada);

            // Assert
            var resultadoOk = resultado as OkObjectResult;
            Assert.IsNotNull(resultadoOk);
            Assert.AreEqual(StatusCodes.Status200OK, resultadoOk.StatusCode);
            Assert.AreEqual(tarefaAtualizada, resultadoOk.Value);
        }

        [Test]
        public async Task RemoverTarefa_TarefaExistente_RetornaNoContent()
        {
            // Arrange
            var tarefaId = 1;
            _tarefaServicoMock.Setup(s => s.RemoverTarefaAsync(tarefaId)).Returns(Task.CompletedTask);

            // Act
            var resultado = await _tarefasController.RemoverTarefa(tarefaId);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(resultado);
        }
    }
}