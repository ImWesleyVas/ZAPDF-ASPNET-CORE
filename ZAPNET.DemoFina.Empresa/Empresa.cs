using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ZAPNET.DemoFina.Util;
using ZAPNET.DemoFina.Util.Enumerator;

namespace ZAPNET.DemoFina.Empresa
{
    public class Empresa : IEmpresa
    {
        public int Id { get; set; }
        public string Mnemonico { get; set; }
        [Display(Name = "Código do Conglomerado")]
        public int Cong { get; set; }
        [Display(Name = "Código da Empresa")]
        public int Empr { get; set; }
        [Display(Name = "Razão Social")]
        public string RazaoSocial { get; set; }
        [Display(Name = "Nome Fantasia")]
        public string NomeFantasia { get; set; }
        public string Segmento { get; set; }
        public string CNPJ { get; set; }
        [Display(Name = "Inscricao Municipal")]
        public string InscricaoMunicipal { get; set; }
        [Display(Name = "Inscricao Estadual")]
        public string InscricaoEstadual { get; set; }
        public string Nire { get; set; }
        [Display(Name = "Código do Agente Regulador (Bacen, CVM, Susep,...)")]
        public string Id_Bacen_Cvm_Susep { get; set; }
        [Display(Name = "Atributo Institucional")]
        public char AtributoInstitucional { get; set; }

        private List<IEndereco> enderecos;
        private List<IModeloDF> modelosDF;



        public Empresa()
        {
        }

        public Empresa(string mnemonico, int cong, int empr, string razaoSocial, string nomeFantasia, string segmento, string cnpj, string inscricaoMunicipal, string inscricaoEstadual, string nire, string id_Bacen_Cvm_Susep, char atributoInstitucional)
        {
            Mnemonico = mnemonico;
            Cong = cong;
            Empr = empr;
            RazaoSocial = razaoSocial;
            NomeFantasia = nomeFantasia;
            Segmento = segmento;
            CNPJ = cnpj;
            InscricaoMunicipal = inscricaoMunicipal;
            InscricaoEstadual = inscricaoEstadual;
            Nire = nire;
            Id_Bacen_Cvm_Susep = id_Bacen_Cvm_Susep;
            AtributoInstitucional = atributoInstitucional;
        }


        public void addEnderecos(IEndereco endereco)
        {
            enderecos.Add(endereco);

        }

        public void addModelosDF(IModeloDF modeloDF)
        {
            modelosDF.Add(modeloDF);

        }
    }
}
