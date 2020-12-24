using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZAPNET.DemoFina.Models.ModelView
{
    public class RelacaoPeriodoRefModelView
    {
        public PeriodoRef PeriodoRef { get; set; }
        public ContaDF ContaDF { get; set; }
        public Cosif Cosif { get; set; }
        
        public List<Cosif> ListaCosifs { get; set; }

        public RelacaoPeriodoRefModelView()
        {

        }

        public RelacaoPeriodoRefModelView(PeriodoRef periodoRef, ContaDF contaDF, Cosif cosif)
        {
            PeriodoRef = periodoRef;
            ContaDF = contaDF;
            Cosif = cosif;
        }
    }
}
