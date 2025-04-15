using GerenciadorTarefas.Core.Entidades;
using GerenciadorTarefas.Core.Interfaces;
using GerenciadorTarefas.Infraestrutura.Persistencia.Contexto;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorTarefas.Infraestrutura.Persistencia.Repositorios
{
    public class TarefaRepositorio : ITarefaRepositorio
    {
        private readonly AppDbContexto _contexto;

        public TarefaRepositorio(AppDbContexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<Tarefa>> ObterTarefasPorProjetoIdAsync(int projetoId)
        {
            return await _contexto.Tarefas
                .Where(t => t.ProjetoId == projetoId)
                .ToListAsync();
        }

        public async Task<Tarefa> AdicionarTarefaAsync(Tarefa tarefa)
        {
            await _contexto.Tarefas.AddAsync(tarefa);
            await _contexto.SaveChangesAsync();
            return tarefa;
        }

        public async Task<Tarefa> AtualizarTarefaAsync(Tarefa tarefa)
        {
            _contexto.Tarefas.Update(tarefa);
            await _contexto.SaveChangesAsync();
            return tarefa;
        }

        public async Task<bool> RemoverTarefaAsync(int tarefaId)
        {
            var tarefa = await _contexto.Tarefas.FindAsync(tarefaId);
            if (tarefa == null) return false;

            _contexto.Tarefas.Remove(tarefa);
            await _contexto.SaveChangesAsync();
            return true;
        }

        public async Task<List<Tarefa>> ObterTarefasAsync()
        {
            return await _contexto.Tarefas.ToListAsync();
        }
    }
}