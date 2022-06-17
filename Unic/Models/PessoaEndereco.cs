using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Unic.Models
{
    public class PessoaEndereco
    {

        [Key]
        [Required]
        public int ID { get; set; }

        public virtual Pessoa Pessoas { get; set; }
        public virtual Endereco Enderecos { get; set; }

        public int PessoaId { get; set; }
        public int EnderecoId { get; set; }
    }
}
