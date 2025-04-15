using Microsoft.AspNetCore.Mvc;
using GerenciadorTarefas.Aplicacao.Interfaces;
using GerenciadorTarefas.API.DTOs;
using GerenciadorTarefas.Core.Entidades;

namespace GerenciadorTarefas.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjetosController : ControllerBase
    {
        private readonly IProjetoServico _projetoServico;

        public ProjetosController(IProjetoServico projetoServico)
        {
            _projetoServico = projetoServico;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProjetoDTO>> ListarProjetos()
        {
            var projetos = _projetoServico.ListarProjetos();
            return Ok(projetos);
        }

        [HttpPost]
        public ActionResult<ProjetoDTO> CriarProjeto([FromBody] ProjetoDTO projetoDTO)
        {
            if (projetoDTO == null)
            {
                return BadRequest("Dados do projeto não podem ser nulos.");
            }

            var projetoCriado = _projetoServico.CriarProjeto(projetoDTO);
            return CreatedAtAction(nameof(ListarProjetos), new { id = projetoCriado.Id }, projetoCriado);
        }

        [HttpDelete("{id}")]
        public ActionResult RemoverProjeto(int id)
        {
            var resultado = _projetoServico.RemoverProjeto(id);
            if (!resultado)
            {
                return NotFound("Projeto não encontrado ou possui tarefas pendentes.");
            }

            return NoContent();
        }
    }
}