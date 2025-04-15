using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using GerenciadorTarefas.API.Controllers;
using GerenciadorTarefas.Aplicacao.Interfaces;
using GerenciadorTarefas.Core.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GerenciadorTarefas.Testes.Integracao.ControllerTests
{
    public class ProjetosControllerTests
    {
        private ProjetosController _controller;
        private Mock<IProjetoServico> _projetoServicoMock;

        [SetUp]
        public void Setup()
        {
            _projetoServicoMock = new Mock<IProjetoServico>();
            _controller = new ProjetosController(_projetoServicoMock.Object);
        }

        [Test]
        public async Task ListarProjetos_DeveRetornarListaDeProjetos()
        {
            // Arrange
            var projetos = new List<Projeto>
            {
                new Projeto { Id = 1, Nome = "Projeto 1" },
                new Projeto { Id = 2, Nome = "Projeto 2" }
            };
            _projetoServicoMock.Setup(servico => servico.ListarProjetos()).ReturnsAsync(projetos);

            // Act
            var resultado = await _controller.ListarProjetos();

            // Assert
            var resultadoOk = resultado as OkObjectResult;
            Assert.IsNotNull(resultadoOk);
            Assert.AreEqual(StatusCodes.Status200OK, resultadoOk.StatusCode);
            Assert.AreEqual(projetos, resultadoOk.Value);
        }

        [Test]
        public async Task CriarProjeto_DeveRetornarProjetoCriado()
        {
            // Arrange
            var novoProjeto = new Projeto { Id = 3, Nome = "Projeto 3" };
            _projetoServicoMock.Setup(servico => servico.CriarProjeto(It.IsAny<Projeto>())).ReturnsAsync(novoProjeto);

            // Act
            var resultado = await _controller.CriarProjeto(novoProjeto);

            // Assert
            var resultadoCreated = resultado as CreatedAtActionResult;
            Assert.IsNotNull(resultadoCreated);
            Assert.AreEqual(StatusCodes.Status201Created, resultadoCreated.StatusCode);
            Assert.AreEqual(novoProjeto, resultadoCreated.Value);
        }

        [Test]
        public async Task RemoverProjeto_DeveRetornarNoContentQuandoRemovido()
        {
            // Arrange
            int projetoId = 1;
            _projetoServicoMock.Setup(servico => servico.RemoverProjeto(projetoId)).Returns(Task.CompletedTask);

            // Act
            var resultado = await _controller.RemoverProjeto(projetoId);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(resultado);
        }

        [Test]
        public async Task RemoverProjeto_DeveRetornarNotFoundQuandoProjetoNaoExiste()
        {
            // Arrange
            int projetoId = 99;
            _projetoServicoMock.Setup(servico => servico.RemoverProjeto(projetoId)).ThrowsAsync(new KeyNotFoundException());

            // Act
            var resultado = await _controller.RemoverProjeto(projetoId);

            // Assert
            var resultadoNotFound = resultado as NotFoundResult;
            Assert.IsNotNull(resultadoNotFound);
            Assert.AreEqual(StatusCodes.Status404NotFound, resultadoNotFound.StatusCode);
        }
    }
}