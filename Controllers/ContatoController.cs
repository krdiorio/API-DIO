using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _1API.Context;
using _1API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace _1API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ContatoController : ControllerBase
    {

        private readonly AgendaContext _context;
        public ContatoController(AgendaContext context)
        {
            _context = context;
        }

        //Create
        [HttpPost("NovoContato")]
        public IActionResult Create(Contato contato)
        {
            _context.Add(contato);
            _context.SaveChanges();
            return CreatedAtAction(nameof(ObterPorId), new { id = contato.Id }, contato);

        }

        //Delete
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var contato = _context.Contatos.Find(id);

            if (contato == null)
                return NotFound();

            _context.Contatos.Remove(contato);
            _context.SaveChanges();

            return NoContent();
        }

        //Update
        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Contato contato)
        {
            var contatoBanco = _context.Contatos.Find(id);

            if (contatoBanco == null)
                return NotFound();

            contatoBanco.Nome = contato.Nome;
            contatoBanco.Telefone = contato.Telefone;
            contatoBanco.Ativo = contato.Ativo;

            _context.Contatos.Update(contatoBanco);
            _context.SaveChanges();
            return Ok(contatoBanco);


        }


        //Read
        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var contato = _context.Contatos.Find(id);

            if (contato == null)
                return NotFound();

            return Ok(contato);

        }

        [HttpGet("ObterPorNome")]
        public IActionResult ObterPorNome(string nome)
        {

            var contatos = _context.Contatos.Where(x => x.Nome.Contains(nome));

            if (contatos == null)
                return NotFound();

            return Ok(contatos);

        }

        [HttpGet("Listar")]
        public IActionResult GetAll()
        {
            return Ok(_context.Contatos.ToList());
        }


    }
}