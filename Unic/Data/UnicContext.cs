using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unic.Models;

namespace Unic.Data
{
    public class UnicContext : DbContext
    {
        public UnicContext(DbContextOptions<UnicContext> options) : base(options)
        {

        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<PessoaEndereco>()
                .HasOne(PessoaEndereco => PessoaEndereco.Enderecos)
                .WithMany(Endereco => Endereco.PessoaEnderecos)
                .HasForeignKey(PessoaEndereco => PessoaEndereco.EnderecoId);

            builder.Entity<PessoaEndereco>()
                .HasOne(PessoaEndereco => PessoaEndereco.Pessoas)
                .WithMany(Pessoa => Pessoa.PessoaEnderecos)
                .HasForeignKey(PessoaEndereco => PessoaEndereco.PessoaId);

        }

        public DbSet<Pessoa> Pessoa { get; set; }
        public DbSet<Endereco> Endereco { get; set; }

        public DbSet<PessoaEndereco> PessoaEndereco { get; set; }
        

    }
}
