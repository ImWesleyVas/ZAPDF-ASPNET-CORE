namespace ZAPNET.DemoFina.Util
{
    public interface IEndereco
    {
        void AddAddress(string mnemonico, Logradouro logradouro, string descricao, string numero, string complemento, string codigoPostal, string bairro, string cidade, string estado, string pais);
        void ModifyAddress();
        void RemoveAddress();
    }
}