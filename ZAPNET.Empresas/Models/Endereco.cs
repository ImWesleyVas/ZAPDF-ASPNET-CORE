using System.ComponentModel.DataAnnotations;
using ZAPNET.Empresas.Models.Enumerator;

namespace ZAPNET.Empresas.Models
{
    public class Endereco
    {
        public int Id { get; set; }
        public string Mnemonico { get; set; }

        //aeroporto, alameda, área, avenida, campo, chácara, colônia, condomínio, conjunto, distrito, esplanada, estação, estrada, favela, fazenda, feira, jardim, ladeira, lago, lagoa, largo, loteamento, morro, núcleo, parque, passarela, pátio, praça, quadra, recanto, residencial, rodovia, rua, setor, sítio, travessa, trecho, trevo, vale, vereda, via, viaduto, viela, vila.
        public Logradouro Logradouro { get; set; }
        public string Descricao { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        [Display(Name = "Código Postal")]
        public string CodigoPostal { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }

        public Endereco()
        {

        }


        public void AddAddress(string mnemonico, Logradouro logradouro, string descricao, string numero, string complemento, string codigoPostal, string bairro, string cidade, string estado, string pais)
        {
            Mnemonico = mnemonico;
            Logradouro = logradouro;
            Descricao = descricao;
            Numero = numero;
            Complemento = complemento;
            CodigoPostal = codigoPostal;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
            Pais = pais;

        }

        public void RemoveAddress()
        {
        }

        public void ModifyAddress()
        {
        }


    }
}
