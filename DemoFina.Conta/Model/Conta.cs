using System.ComponentModel.DataAnnotations;
using ZAPNET.DemoFina.Util;

namespace ZAPNET.DemoFina.Conta.Models
{
    public class Conta : IConta
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Tipo { get; set; }
        public string Natureza { get; set; }
        [Range(1, 9)]
        public int Nivel { get; set; }
        public string Classe { get; set; }

        public Conta()
        {

        }

        public Conta(int id, string descricao, string tipo, string natureza, int nivel, string classe)
        {
            Id = id;
            Descricao = descricao;
            Tipo = tipo;
            Natureza = natureza;
            Nivel = nivel;
            Classe = classe;

        }

    }
}
