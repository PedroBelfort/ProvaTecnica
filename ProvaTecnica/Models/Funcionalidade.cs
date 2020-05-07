using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProvaTecnica.Models
{
    public class Funcionalidade
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório.")]
        public string Nome { get; set; }

        public virtual List<PerfilFuncionalidade> PerfilFuncionalidades { get; set; }



    }
}
