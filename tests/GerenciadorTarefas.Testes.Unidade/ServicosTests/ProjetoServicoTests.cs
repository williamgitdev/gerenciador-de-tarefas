using Xunit;
using GerenciadorTarefas.Aplicacao.Servicos;
using GerenciadorTarefas.Core.Entidades;
using GerenciadorTarefas.Core.Interfaces;
using Moq;
using System.Collections.Generic;

namespace GerenciadorTarefas.Testes.Unidade.ServicosTests
{
    public class ProjetoServicoTests
    {
        private readonly Mock<IProjetoRepositorio> _projetoRepositorioMock;
        private readonly ProjetoServico _projetoServico;

        public ProjetoServicoTests()
        {
            _projetoRepositorioMock = new Mock<IProjetoRepositorio>();
            _projetoServico = new ProjetoServico(_projetoRepositorioMock.Object);
        }

        [Fact]
        public void CriarProjeto_DeveAdicionarNovoProjeto()
        {
            // Arrange
            var projeto = new Projeto { Id = 1, Nome = "Projeto Teste" };
            _projetoRepositorioMock.Setup(r => r.Adicionar(projeto)).Returns(projeto);

            // Act
            var resultado = _projetoServico.CriarProjeto(projeto);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(projeto.Nome, resultado.Nome);
            _projetoRepositorioMock.Verify(r => r.Adicionar(projeto), Times.Once);
        }

        [Fact]
        public void ListarProjetos_DeveRetornarListaDeProjetos()
        {
            // Arrange
            var projetos = new List<Projeto>
            {
                new Projeto { Id = 1, Nome = "Projeto 1" },
                new Projeto { Id = 2, Nome = "Projeto 2" }
            };
            _projetoRepositorioMock.Setup(r => r.Listar()).Returns(projetos);

            // Act
            var resultado = _projetoServico.ListarProjetos();

            // Assert
            Assert.Equal(2, resultado.Count);
            _projetoRepositorioMock.Verify(r => r.Listar(), Times.Once);
        }

        [Fact]
        public void RemoverProjeto_DeveRemoverProjetoExistente()
        {
            // Arrange
            var projeto = new Projeto { Id = 1, Nome = "Projeto Teste" };
            _projetoRepositorioMock.Setup(r => r.ObterPorId(projeto.Id)).Returns(projeto);
            _projetoRepositorioMock.Setup(r => r.Remover(projeto));

            // Act
            _projetoServico.RemoverProjeto(projeto.Id);

            // Assert
            _projetoRepositorioMock.Verify(r => r.Remover(projeto), Times.Once);
        }
    }
}