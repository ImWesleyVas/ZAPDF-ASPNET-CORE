using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZAPNET.DemoFina.Models;
using ZAPNET.DemoFina.Models.ModelView;
using ZAPNET.DemoFina.Models.Enumerator;
using ZAPNET.DemoFina.DAL;

namespace ZAPNET.DemoFina.Controllers
{
    public class EmpresaController : Controller
    {
        

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ListaEmpresas()
        {
         
            List<Empresa> empresas = new EmpresaDAL().FindAllEmpresas();

            return View(empresas);

        }

        public IActionResult CadastroEmpresa()
        {
            return View();            
        }

        

        [HttpPost]
        public IActionResult CadastroEmpresa(Empresa formulario)
        {
            Empresa empresa = new Empresa();
            Endereco endereco = new Endereco();
            EmpresaModelView cadEmpresa = new EmpresaModelView();

            cadEmpresa.Empresa = empresa;
            cadEmpresa.Endereco = endereco;

            empresa.Mnemonico = formulario.Mnemonico.ToString();
            empresa.Cong = int.Parse(formulario.Cong.ToString());
            empresa.Empr = int.Parse(formulario.Cong.ToString());
            empresa.RazaoSocial = formulario.RazaoSocial;
            empresa.NomeFantasia = formulario.NomeFantasia;
            empresa.Segmento = formulario.Segmento;
            empresa.CNPJ = formulario.CNPJ;
            empresa.InscricaoMunicipal = formulario.InscricaoMunicipal;
            empresa.InscricaoEstadual = formulario.InscricaoEstadual;
            empresa.Nire = formulario.Nire;
            empresa.Id_Bacen_Cvm_Susep = formulario.Id_Bacen_Cvm_Susep;
            empresa.AtributoInstitucional = formulario.AtributoInstitucional;

            

            return View();
        }

        public IActionResult CadastroEndereco()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CadastroEndereco(Endereco formEndereco)
        {
            Endereco endereco = new Endereco();
            endereco.AddAddress(
                formEndereco.Mnemonico,
                (Logradouro)Enum.Parse(typeof(Logradouro), formEndereco.Logradouro.ToString()), // converter string em enum
                formEndereco.Descricao.ToString(),
                formEndereco.Numero.ToString(),
                formEndereco.Complemento.ToString(),
                formEndereco.CodigoPostal.ToString(),
                formEndereco.Bairro.ToString(),
                formEndereco.Cidade.ToString(),
                formEndereco.Estado.ToString(),
                formEndereco.Pais.ToString());

            return View();
        }


    }
}
