using System.Collections.Generic;
using System.Threading.Tasks;

namespace ZAPNET.DemoFina.Services
{
    public interface ICadocRepository
    {
        Task<List<string[]>> FindAllCadocAsync();
        Task<bool> GravarCadocDBAsync(List<string[]> linhaCadoc);
        Task<bool> GravarCadocErrorDBAsync(List<string[]> linhaCadoc);
    }
}