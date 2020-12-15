
namespace ZAPNET.DemoFina.Models
{
    public class ContaRubricaDF : Conta
    {

        private ModeloDF modeloDF = new ModeloDF();

        public ContaRubricaDF()
        {
        }

        public ContaRubricaDF(int id, string descricao, string tipo, string natureza, int nivel, string classe) : base(id, descricao, tipo, natureza, nivel, classe)
        {
        }
    }
}
