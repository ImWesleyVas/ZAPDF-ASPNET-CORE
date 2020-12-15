using System;

namespace ZAPNET.DemoFina.Models
{
    public class Cosif : Conta
    {
        public DateTime Validade { get; set; }
        public string AtributoInstitucional { get; set; }

        public Cosif() : base()
        {

        }

        public Cosif(int id, string descricao, string tipo, string natureza, int nivel, string classe, DateTime validade,
                        string atributoInstitucional) : base(id, descricao, tipo, natureza, nivel, classe)
        {
            Validade = validade;
            AtributoInstitucional = atributoInstitucional;
        }


    }
}