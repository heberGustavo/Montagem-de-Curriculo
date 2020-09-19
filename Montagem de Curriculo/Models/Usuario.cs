using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Montagem_de_Curriculo.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "Campo obrigátorio!")]
        [StringLength(50, ErrorMessage = "Use menos caracteres!")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        [Remote("UsuarioExiste", "Usuarios")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo obrigátorio!")]
        [StringLength(50, ErrorMessage = "Use menos caracteres!")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        //Esta relacionada a varias info. de Login e Curriculos
        public ICollection<InformacaoLogin> InformacoesLogin { get; set; }
        public ICollection<Curriculo> Curriculos { get; set; }
    }
}
