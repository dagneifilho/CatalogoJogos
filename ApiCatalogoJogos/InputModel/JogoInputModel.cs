using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoJogos.InputModel {
    public class JogoInputModel {

       [Required]
       [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do jogo deve conter de 3 a 100 caracteres.")]

       public string Nome { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome da produtora deve conter de 3 a 100 caracteres.")]
        public string Produtora { get; set; }

        [Required]
        [Range(1, 1000, ErrorMessage = "O preço deve ser de no mínimo 1 real e no máximo 1000.")]
        public double Preco { get; set; }
    }
}
