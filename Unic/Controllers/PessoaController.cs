using Unic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unic.Data;
using Unic.Data.Dtos;
using AutoMapper;

namespace Unic.Controllers
{
    
    public class PessoaController : Controller
    {
        private UnicContext _context;
        private IMapper _mapper;

        public PessoaController(UnicContext unicContext, IMapper mapper)
        {
            _context = unicContext;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("AdicionarPessoa")]
        public IActionResult AdicionarPessoa([FromBody] CreatePessoaDto pessoaDto)
        {
            Pessoa pessoa = _mapper.Map<Pessoa>(pessoaDto);

            pessoa.DataCriaCao = DateTime.Now;
            _context.Pessoa.Add(pessoa);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperarPessoa), new { id = pessoa.Id, pessoa });

        }

        [HttpGet("{id}")]
        [Route("RecuperarPessoa")]
        public IActionResult RecuperarPessoa(int id)
        {

            Pessoa pessoa = _context.Pessoa.FirstOrDefault(p => p.Id == id);
            var pessoaDto = _mapper.Map<RecuperarPessoaDto>(pessoa);
            if (pessoa != null)
            {
                return Ok(pessoaDto);
            }

            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult Editar(int id, [FromBody] AlterarPessoaDto PessoaEditada)
        {
            
            Pessoa pessoa = _context.Pessoa.FirstOrDefault(p => p.Id == id);
            if(pessoa == null)
            {
                return NotFound();
            }

            pessoa = _mapper.Map<Pessoa>(PessoaEditada);

            _context.SaveChanges();
            
            return Ok(pessoa);
        }


        [HttpGet]
        [Route("ListarPessoa")]
        public IActionResult ListarPessoa()
        {
            var pessoas = _context.Pessoa;
            List<ListarPessoaDto> pessoaDto = new();
            foreach (var p in pessoas)
            {
                pessoaDto.Add(_mapper.Map<ListarPessoaDto>(p));
            }
            
            return Json(pessoaDto);
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
