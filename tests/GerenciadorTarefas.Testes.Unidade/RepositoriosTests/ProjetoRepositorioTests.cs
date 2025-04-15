using Xunit;
using GerenciadorTarefas.Infraestrutura.Persistencia.Repositorios;
using GerenciadorTarefas.Core.Entidades;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace GerenciadorTarefas.Testes.Unidade.RepositoriosTests
{
    public class ProjetoRepositorioTests
    {
        private readonly ProjetoRepositorio _repositorio;

        public ProjetoRepositorioTests()
        {
            // Inicializa o repositório com um contexto de banco de dados em memória ou simulado
            _repositorio = new ProjetoRepositorio(/* Passar o contexto aqui */);
        }

        [Fact]
        public async Task Deve_Criar_Projeto()
        {
            var projeto = new Projeto { Nome = "Projeto Teste" };

            await _repositorio.Adicionar(projeto);
            var projetoSalvo = await _repositorio.ObterPorId(projeto.Id);

            Assert.NotNull(projetoSalvo);
            Assert.Equal("Projeto Teste", projetoSalvo.Nome);
        }

        [Fact]
        public async Task Deve_Listar_Projetos()
        {
            var projetos = await _repositorio.Listar();

            Assert.IsType<List<Projeto>>(projetos);
        }

        [Fact]
        public async Task Deve_Remover_Projeto()
        {
            var projeto = new Projeto { Nome = "Projeto para Remover" };
            await _repositorio.Adicionar(projeto);

            await _repositorio.Remover(projeto.Id);
            var projetoRemovido = await _repositorio.ObterPorId(projeto.Id);

            Assert.Null(projetoRemovido);
        }

        [Fact]
        public async Task Nao_Deve_Remover_Projeto_Com_Tarefas_Pendentes()
        {
            var projeto = new Projeto { Nome = "Projeto com Tarefas Pendentes" };
            await _repositorio.Adicionar(projeto);
            // Adicionar lógica para criar tarefas pendentes associadas ao projeto

            await Assert.ThrowsAsync<Exception>(async () => await _repositorio.Remover(projeto.Id));
        }
    }
}