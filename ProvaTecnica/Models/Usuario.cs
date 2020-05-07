using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProvaTecnica.Models
{
    public class Usuario
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório.")]
        public string Senha { get; set; }

       
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório.")]
        public int PerfilId { get; set; }

        public virtual Perfil Perfil { get; set; }

    }
}
