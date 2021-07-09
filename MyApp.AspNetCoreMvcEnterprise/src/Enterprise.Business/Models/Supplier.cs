using System;
using System.Collections.Generic;

namespace Enterprise.Business.Models
{
    public class Supplier : Entity
    {
        public string Nome { get; set; }
        public string Documento { get; set; }
        public TipoFornecedor TipoFornecedor { get; set; }
        public Address Endereco { get; set; }
        public bool Ativo { get; set; }
        public IEnumerable<Product> Products { get; set; }

        public Supplier() : base() { }

        public Supplier(string nome, string documento, TipoFornecedor tipoFornecedor, Address endereco, bool ativo) : base()
        {
            Nome = nome;
            Documento = documento;
            TipoFornecedor = tipoFornecedor;
            Endereco = endereco;
            Ativo = ativo;
        }

        public override bool Equals(object obj)
        {
            return obj is Supplier supplier &&
                   Id.Equals(supplier.Id);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }

    public enum TipoFornecedor
    {
        PessoaFisica = 1,
        PessoaJuridica = 2
    }

}
