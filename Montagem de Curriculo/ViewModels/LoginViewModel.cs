using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Montagem_de_Curriculo.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Campo obrigátorio!")]
        [StringLength(50, ErrorMessage = "Use menos caracteres!")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo obrigátorio!")]
        [StringLength(50, ErrorMessage = "Use menos caracteres!")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}