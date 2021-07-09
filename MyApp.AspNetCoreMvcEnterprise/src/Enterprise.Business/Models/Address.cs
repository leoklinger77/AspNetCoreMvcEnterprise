using System;

namespace AppMvc.Models
{
    public class Address : Entity
    {
        public Supplier Supplier { get; set; }
        public Guid SupplierId { get; set; }                
        public string Logradouro { get; set; }        
        public string Numero { get; set; }        
        public string Complemento { get; set; }        
        public string Cep { get; set; }        
        public string Bairro { get; set; }        
        public string Cidade { get; set; }                
        public string Estado { get; set; }

        public Address() : base() { }

        public Address(Supplier supplier, string logradouro, string numero, string complemento, string cep, string bairro, string cidade, string estado) : base()
        {
            Supplier = supplier;
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Cep = cep;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
        }

        public override bool Equals(object obj)
        {
            return obj is Address address &&
                   Id.Equals(address.Id);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
