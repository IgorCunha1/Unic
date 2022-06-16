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
            builder.Entity<Endereco>()
                .HasOne(endereco => endereco.Pessoa)
                .WithOne(pessoa => pessoa.Endereco)
                .HasForeignKey<Pessoa>(pessoa => pessoa.EnderecoId);
        }

        public DbSet<Pessoa> Pessoa { get; set; }
        public DbSet<Endereco> Endereco { get; set; }
        

    }
}
