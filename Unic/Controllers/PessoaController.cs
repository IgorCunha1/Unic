using Unic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unic.Data;


namespace Unic.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PessoaController : Controller
    {
        private UnicContext _context;


        public PessoaController(UnicContext unicContext)
        {
            _context = unicContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AdicionarPessoa([FromBody] Pessoa pessoa)
        {
            pessoa.DataCriaCao = DateTime.Now;
            _context.Pessoa.Add(pessoa);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperarPessoa), new { id = pessoa.Id, pessoa });

        }

        [HttpGet("{id}")]
        public IActionResult RecuperarPessoa(int id)
        {

            Pessoa pessoa = _context.Pessoa.FirstOrDefault(p => p.Id == id);
            if (pessoa != null)
            {
                return Ok(pessoa);
            }

            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult Editar(int id, [FromBody] Pessoa PessoaEditada)
        {
            
            Pessoa pessoa = _context.Pessoa.FirstOrDefault(p => p.Id == id);
            if(pessoa == null)
            {
                return NotFound();
            }
            pessoa.Cpf = PessoaEditada.Cpf;
            pessoa.NomeCompleto = PessoaEditada.NomeCompleto;

            _context.SaveChanges();
            
            return Ok(pessoa);
        }


        [HttpGet]
        public IEnumerable<Pessoa> ListarPessoa()
        {
            return _context.Pessoa;
        }
        
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            try { 

                Pessoa pessoa = _context.Pessoa.FirstOrDefault(p => p.Id == id);
                if(pessoa != null)
                {
                    _context.Pessoa.Remove(pessoa);
                    _context.SaveChanges();
                    return Ok();
                }
                else
                {
                    return Json("Cliente não Encontrado");
                }
                
            }
            catch (Exception ex)
            {
               return Json(ex.Message);
            }
        }


    }
}
