using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProvaTecnica.Models
{
    public class PerfilFuncionalidade
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório.")]
        public int PerfilId { get; set; }

        public Perfil Perfil { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório.")]
        public int FuncionalidadeId { get; set; }

        public Funcionalidade Funcionalidade { get; set; }
    }
}
