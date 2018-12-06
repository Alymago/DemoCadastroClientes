using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Meu_primeiro_projeto.Models
{
    public class Clientes
    {
        public Clientes()
        {
            ClientesId = Guid.NewGuid();
        }
        public Guid ClientesId { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public int Telefone { get; set; }
        public string Observacao { get; set; }

    }
}