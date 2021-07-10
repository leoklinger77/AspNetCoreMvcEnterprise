using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Enterprise.App.ViewModels
{
    public class ProductViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public SupplierViewModel Supplier { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Fornecedor")]
        public Guid SupplierId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(255, ErrorMessage = "O campo {0} deve ter entre {1} e {2} caracteres", MinimumLength = 2)]
        public string Nome { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} deve ter entre {1} e {2} caracteres", MinimumLength = 2)]
        public string Descricao { get; set; }

        [Display(Name = "Imagem")]
        public IFormFile ImageUpload { get; set; }
        public string Image { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public decimal Valor { get; set; }

        [ScaffoldColumn(false)]
        public DateTime DataCadastro { get; set; }

        [Display(Name = "Ativo?")]
        public bool Ativo { get; set; }

        public IEnumerable<SupplierViewModel> SupplierList { get; set; }
    }
}
