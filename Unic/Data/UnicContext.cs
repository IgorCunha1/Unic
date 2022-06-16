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
        //up
        public DbSet<Pessoa> Pessoa { get; set; }
        public DbSet<Endereco> Endereco { get; set; }
        

    }
}
