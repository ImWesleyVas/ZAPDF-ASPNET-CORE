using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZAPNET.DemoFina.Models;
using ZAPNET.DemoFina.Models.ModelView;
using ZAPNET.DemoFina.Models.Enumerator;
using ZAPNET.DemoFina.DAL;
using ZAPNET.DemoFina.Services;

namespace ZAPNET.DemoFina.Controllers
{
    public class EmpresaController : Controller
    {
        ICrudRepository<Empresa> _repo;
        EmpresaDAL _empresaDal;

        public EmpresaController(ICrudRepository<Empresa> repo)
        {
            _repo = repo;
            _empresaDal = new EmpresaDAL(_repo);
        }

        



        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ListaEmpresas(int? id)
        {
         
           return View(await (new EmpresaDAL(_repo).FindAllEmpresas(id)));

        }

        public IActionResult CadastroEmpresa()
        {
            return View();            
        }

        

        [HttpPost]
        public async Task<IActionResult> CadastroEmpresa(EmpresaModelView formulario)
        {
             /*Empresa empresa = new Empresa();
             Endereco endereco = new Endereco();
             EmpresaModelView cadEmpresa = new EmpresaModelView();

             cadEmpresa.Empresa = empresa;
             cadEmpresa.Endereco = endereco;

             empresa.Mnemonico = formulario.Empresa.Mnemonico.ToString();
             empresa.Cong = int.Parse(formulario.Empresa.Cong.ToString());
             empresa.Empr = int.Parse(formulario.Empresa.Cong.ToString());
             empresa.RazaoSocial = formulario.Empresa.RazaoSocial;
             empresa.NomeFantasia = formulario.Empresa.NomeFantasia;
             empresa.Segmento = formulario.Empresa.Segmento;
             empresa.CNPJ = formulario.Empresa.CNPJ;
             empresa.InscricaoMunicipal = formulario.Empresa.InscricaoMunicipal;
             empresa.InscricaoEstadual = formulario.Empresa.InscricaoEstadual;
             empresa.Nire = formulario.Empresa.Nire;
             empresa.Id_Bacen_Cvm_Susep = formulario.Empresa.Id_Bacen_Cvm_Susep;
             empresa.AtributoInstitucional = formulario.Empresa.AtributoInstitucional;*/



            _empresaDal = new EmpresaDAL(_repo);

            try
            {
                if (ModelState.IsValid)
                {
                    if (await _empresaDal.Salvar(formulario.Empresa))
                    {
                        ViewData["Sucesso"] = "Empresa cadastrada com sucesso!!!";
                        return RedirectToAction("ListaEmpresas");
                    }
                    else
                    {
                        ViewData["Falha"] = "O cadastro da empresa não foi realizado!!!";
                        return View();
                    }

                    // retorna e mantém os dados nos campos - não recarrega a pagina
                    
                }
            }
            catch (Exception e)
            {

                ViewData["Falha"] = "[ERROR] - Verifique: " + e.Message;
            }

            // Redireciona para a ActionResult ListaEmpresas que responde
            // comando (verbo) do HttpGet
            return RedirectToAction("ListaEmpresas");

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
