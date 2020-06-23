using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace EventopWebAPI.Controllers
{
    public class CORREIOsController : ApiController
    {
        public Object GetCORREIOS(string cep)
        {
            WebAPICorreios.AtendeClienteClient webCorreios = new WebAPICorreios.AtendeClienteClient("AtendeClientePort");

            try{
                var dadosDoCep = webCorreios.consultaCEP(cep);
                Object[] dados = { dadosDoCep.cidade, dadosDoCep.bairro, dadosDoCep.end };
                return Ok(dados);
            }
            catch {
                return null;
            }
            
        }
    }
}