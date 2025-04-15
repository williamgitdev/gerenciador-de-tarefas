using Microsoft.AspNetCore.Mvc;
using GerenciadorTarefas.Aplicacao.Interfaces;
using GerenciadorTarefas.API.DTOs;
using GerenciadorTarefas.Core.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GerenciadorTarefas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefasController : ControllerBase
    {
        private readonly ITarefaServico _tarefaServico;

        public TarefasController(ITarefaServico tarefaServico)
        {
            _tarefaServico = tarefaServico;
        }

        [HttpGet("{projetoId}")]
        public async Task<ActionResult<IEnumerable<TarefaDTO>>> ObterTarefasPorProjeto(int projetoId)
        {
            var tarefas = await _tarefaServico.ObterTarefasPorProjeto(projetoId);
            return Ok(tarefas);
        }

        [HttpPost]
        public async Task<ActionResult<TarefaDTO>> CriarTarefa([FromBody] TarefaDTO tarefaDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tarefaCriada = await _tarefaServico.CriarTarefa(tarefaDto);
            return CreatedAtAction(nameof(ObterTarefasPorProjeto), new { projetoId = tarefaCriada.ProjetoId }, tarefaCriada);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TarefaDTO>> AtualizarTarefa(int id, [FromBody] TarefaDTO tarefaDto)
        {
            if (id != tarefaDto.Id)
                return BadRequest();

            var tarefaAtualizada = await _tarefaServico.AtualizarTarefa(tarefaDto);
            if (tarefaAtualizada == null)
                return NotFound();

            return Ok(tarefaAtualizada);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoverTarefa(int id)
        {
            var resultado = await _tarefaServico.RemoverTarefa(id);
            if (!resultado)
                return NotFound();

            return NoContent();
        }
    }
}