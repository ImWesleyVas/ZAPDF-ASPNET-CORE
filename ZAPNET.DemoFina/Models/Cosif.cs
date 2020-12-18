using System;

namespace ZAPNET.DemoFina.Models
{
    public class Cosif : Conta
    {
        public int ContaCosif { get; set; }
        public string Validade { get; set; }
        public string AtributoInstitucional { get; set; }

        public Cosif() : base()
        {

        }

        public Cosif(int id, int contaCosif, string descricao, string tipo, string natureza, int nivel, string classe, string validade,
                        string atributoInstitucional) : base(id, descricao, tipo, natureza, nivel, classe)
        {
            ContaCosif = contaCosif;
            Validade = validade;
            AtributoInstitucional = atributoInstitucional;
        }


    }
}