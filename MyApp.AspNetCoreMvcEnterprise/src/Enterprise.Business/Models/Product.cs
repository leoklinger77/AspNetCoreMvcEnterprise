using System;

namespace AppMvc.Models
{
    public class Product : Entity
    {
        public Supplier Supplier { get; set; }
        public Guid SupplierId { get; set; }        
        public string Nome { get; set; }        
        public string Descricao { get; set; }        
        public string Image { get; set; }        
        public decimal Valor { get; set; }        
        public DateTime DataCadastro { get; set; }        
        public bool Ativo { get; set; }
        public Product() : base() { }

        public Product(Supplier supplier, string nome, string descricao, string image, decimal valor, DateTime dataCadastro, bool ativo) : base()
        {
            Supplier = supplier;
            Nome = nome;
            Descricao = descricao;
            Image = image;
            Valor = valor;
            DataCadastro = dataCadastro;
            Ativo = ativo;
        }

        public override bool Equals(object obj)
        {
            return obj is Product product &&
                   Id.Equals(product.Id);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
