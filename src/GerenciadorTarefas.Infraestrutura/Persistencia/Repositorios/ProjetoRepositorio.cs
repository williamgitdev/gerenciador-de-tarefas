using GerenciadorTarefas.Core.Entidades;
using GerenciadorTarefas.Core.Interfaces;
using GerenciadorTarefas.Infraestrutura.Persistencia.Contexto;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorTarefas.Infraestrutura.Persistencia.Repositorios
{
    public class ProjetoRepositorio : IProjetoRepositorio
    {
        private readonly AppDbContexto _contexto;

        public ProjetoRepositorio(AppDbContexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<Projeto>> ListarProjetosAsync()
        {
            return await _contexto.Projetos.ToListAsync();
        }

        public async Task<Projeto> ObterProjetoPorIdAsync(int id)
        {
            return await _contexto.Projetos.FindAsync(id);
        }

        public async Task CriarProjetoAsync(Projeto projeto)
        {
            await _contexto.Projetos.AddAsync(projeto);
            await _contexto.SaveChangesAsync();
        }

        public async Task AtualizarProjetoAsync(Projeto projeto)
        {
            _contexto.Projetos.Update(projeto);
            await _contexto.SaveChangesAsync();
        }

        public async Task RemoverProjetoAsync(int id)
        {
            var projeto = await ObterProjetoPorIdAsync(id);
            if (projeto != null)
            {
                _contexto.Projetos.Remove(projeto);
                await _contexto.SaveChangesAsync();
            }
        }
    }
}