using Enterprise.Business.Models.Validations.Documents;
using FluentValidation;

namespace Enterprise.Business.Models.Validations
{
    public class SupplierValidation : AbstractValidator<Supplier>
    {
        public SupplierValidation()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("O Campo {PropertyName} precisa ser fornecido.")
                .Length(2, 100)
                .WithMessage("O Campo {PropertyName} precisa ter entre {MinLength} e {MaxLangth} caracteres");

            When(x => x.TipoFornecedor == TipoFornecedor.PessoaFisica, () =>
            {
                RuleFor(f => f.Documento.Length).Equal(11)
                    .WithMessage("O campo Documento precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}.");
                
                RuleFor(x => ValidationDocuments.IsCpf(x.Documento))
                .Equal(true)
                .WithMessage("O campo {PropertyName} não é valido");
            });

            When(x => x.TipoFornecedor == TipoFornecedor.PessoaJuridica, () =>
            {
                RuleFor(f => f.Documento.Length).Equal(14)
                    .WithMessage("O campo Documento precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}.");
                
                RuleFor(x => ValidationDocuments.IsCnpj(x.Documento))
                .Equal(true)
                .WithMessage("O campo {PropertyName} não é valido");
            });
        }
    }
}
