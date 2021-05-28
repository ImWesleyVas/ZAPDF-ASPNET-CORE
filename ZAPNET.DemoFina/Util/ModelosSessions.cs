using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZAPNET.DemoFina.Util
{
    public class ModelosSessions : IModeloSessions
        /****************  Métodos ********************
            GET<nome>  - obter o dado de sessão
            SET<nome>  - incluir o dado na sessão  
        **********************************************/
    {

        private readonly IHttpContextAccessor contextAccessor;

        public ModelosSessions(IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
        }

        /*********** PERIODO *************/
        public string GetPeriodo()
        {
            return contextAccessor.HttpContext.Session.GetString("periodo");
        }

        public void SetPeriodo(string periodo)
        {
            contextAccessor.HttpContext.Session.SetString("periodo", periodo);
        }


        //*********** MODELO *************/
        public int? GetModeloId()
        {
            return contextAccessor.HttpContext.Session.GetInt32("modeloID");
        }

        public void SetModeloID(int modeloID)
        {
            contextAccessor.HttpContext.Session.SetInt32("modeloID", modeloID);
        }




    }
}
