using dio_api_desafio.Context;
using dio_api_desafio.Entities;
using dio_api_desafio.Models;
using Microsoft.AspNetCore.Mvc;

namespace dio_api_desafio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly OrganizadorContext _context;

        public TarefaController(OrganizadorContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult CriarTarefa(Tarefa tarefa)
        {
            _context.Tarefas.Add(tarefa);
            _context.SaveChanges();

            return CreatedAtAction(nameof(ObterPorId), new {id = tarefa.Id}, tarefa);
        }

        [HttpGet("ObterTodos")]
        public IActionResult ObterTodos()
        {
            var tarefas = _context.Tarefas.Where(x => x != null);
            // if (tarefas == null)
            //     return NotFound();
            
            return Ok(tarefas);
        }

        [HttpGet("ObterPorId/{id}")]
        public IActionResult ObterPorId(int id)
        {   
            var tarefa = _context.Tarefas.Find(id);
            if (tarefa == null)
                return NotFound();

            return Ok(tarefa);
        }

        [HttpGet("ObterPorTitulo/{titulo}")]
        public IActionResult ObterPorTitulo(string titulo)
        {
            var tarefas = _context.Tarefas.Where(x => x.Titulo.Contains(titulo));
            // if (tarefas == null)
            //     return NotFound();

            return Ok(tarefas);
        }

        [HttpGet("ObterPorData/{data}")]
        public IActionResult ObterPorData(DateTime data)
        {
            var tarefas = _context.Tarefas.Where(x => x.Data.Date == data.Date);
            // if (tarefas == null)
            //     return NotFound();

            return Ok(tarefas);
        }

        [HttpGet("ObterPorStatus/{status}")]
        public IActionResult ObterPorStatus(EnumStatusTarefa status)
        {
            var tarefas = _context.Tarefas.Where(x => x.Status == status);
            // if (tarefas == null)
            //     return NotFound();

            return Ok(tarefas);
        }

        [HttpPut("Atulizar")]
        public IActionResult Atualizar(int id, Tarefa tarefa)
        {
            var tarefas = _context.Tarefas.Find(id);
            if(tarefas == null)
                return NotFound();

            if(tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data tarefa não pode ser vazia"});

            tarefas.Titulo = tarefa.Titulo;
            tarefas.Descricao = tarefa.Descricao;
            tarefas.Data = tarefa.Data;
            tarefas.Status = tarefa.Status;
            _context.SaveChanges();

            return Ok(tarefas);
        }

        [HttpDelete("Apagar")]
        public IActionResult Apagar(int id)
        {
            var tarefa = _context.Tarefas.Find(id);
            if(tarefa == null)
                return NotFound();

            _context.Tarefas.Remove(tarefa);
            _context.SaveChanges();

            return NoContent();
        }
    }
}