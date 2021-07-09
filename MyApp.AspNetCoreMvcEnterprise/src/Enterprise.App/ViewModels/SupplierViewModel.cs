using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Enterprise.App.ViewModels
{
    public class SupplierViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(255, ErrorMessage = "O campo {0} deve ter entre {1} e {2} caracteres", MinimumLength = 2)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(14, ErrorMessage = "O campo {0} deve ter entre {1} e {2} caracteres", MinimumLength = 11)]
        public string Documento { get; set; }

        [Display(Name ="Tipo")]
        public int TipoFornecedor { get; set; }
        public AddressViewModel Endereco { get; set; }

        [Display(Name = "Ativo?")]
        public bool Ativo { get; set; }

        public IEnumerable<ProductViewModel> Products { get; set; }

    }
}
