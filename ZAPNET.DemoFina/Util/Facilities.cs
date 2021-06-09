using System.Text.RegularExpressions;

namespace ZAPNET.DemoFina.Util
{
    public static class Facilities
    {
        public static bool ehNumero(string valor, string qteCaract)
        {
            return Regex.IsMatch(valor, $"[0-9]{'{'+qteCaract+'}'}");
        }
    }
}
