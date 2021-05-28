using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZAPNET.DemoFina.Models.ModelView
{
    public class ModeloDFModelView
    {
        public ModeloDF ModeloDF { get; set; }
        public ContaDF ContaDF { get; set; }
        public PeriodoRef PeriodoRef { get; set; }

        public List<ContaDF> ListaContas { get; set; }
    }
}
