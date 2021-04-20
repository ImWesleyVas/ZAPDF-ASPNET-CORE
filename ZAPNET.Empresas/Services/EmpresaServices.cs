using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZAPNET.DAL.DAL;
using ZAPNET.Empresas.Models;
using ZAPNET.Interfaces;

namespace ZAPNET.Empresas.Services
{
    public class EmpresaServices
    {
        private ICrudRepository<string[]> repo = new EmpresaRepository();

        public async Task<List<Empresa>> FindAllEmpresas(int? id)
        {
            var empresaString = await repo.FindAll(id);

            var _empresas = new List<Empresa>();

            try
            {
                foreach (var empresa in empresaString)
                {
                    var _empresa = new Empresa();

                    _empresa.Mnemonico = empresa[0].ToString().Trim();


                    _empresas.Add(_empresa);

                    /// continuar daqui
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return _empresas;
        }

        public async Task<bool> Salvar(Empresa empresa)
        {
            return await repo.Add(empresa);
        }
    }
}
