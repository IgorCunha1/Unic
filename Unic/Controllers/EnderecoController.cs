﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unic.Data;
using Unic.Data.Dtos;
using Unic.Models;

namespace Unic.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnderecoController : Controller
    {
        private UnicContext _context;
        private IMapper _mapper;

        public EnderecoController(UnicContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AdicionarEndereco([FromBody] CreateEnderecoDto enderecoDto)
        {
            var endereco = _mapper.Map<Endereco>(enderecoDto);
            _context.Endereco.Add(endereco);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperarEndereco), new { id = endereco.Id, endereco });
        }

        [HttpPut("{id}")]
        public IActionResult Editar(int id, [FromBody] AlterarEnderecoDto enderecoDto)
        {
            var endereco = _context.Endereco.FirstOrDefault(e => e.Id == id);
            if(endereco != null)
            {
                endereco = _mapper.Map<Endereco>(enderecoDto);
                _context.SaveChanges();

                return Ok(endereco);
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var endereco = _context.Endereco.FirstOrDefault(e => e.Id == id);
            if(endereco != null)
            {
                _context.Endereco.Remove(endereco);
                _context.SaveChanges();
                return Ok();
            }
            return NotFound();
        }


        [HttpGet]
        public IActionResult ListarEnderecos()
        {
            var enderecos = _context.Endereco;
            List<ListarEnderecoDto> enderecosDto = new();
            foreach(var e in enderecos)
            {
                enderecosDto.Add(_mapper.Map<ListarEnderecoDto>(e));
            }

            return Json(enderecosDto);

        }

        [HttpGet("{id}")]
        public IActionResult RecuperarEndereco(int id)
        {
            var endereco = _context.Endereco.FirstOrDefault(e => e.Id == id);
            var enderecoDto = _mapper.Map<RecuperarEnderecoDto>(endereco);

            return Ok(enderecoDto);

        }



    }
}
