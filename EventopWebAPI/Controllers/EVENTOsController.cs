using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using EventopWebAPI.Models;

namespace EventopWebAPI.Controllers
{
    public class EVENTOsController : ApiController
    {
        private EventopEntities db = new EventopEntities();

        // GET: api/EVENTOs
        [ResponseType(typeof(EVENTO))]
        public IHttpActionResult GetEVENTO()
        {
            var eVENTO = (from even in db.EVENTO 
                          join galeve in db.GALERIA_EVENTO on even.EVEN_ID_EVENTO equals galeve.EVEN_ID_EVENTO
                          join img in db.GALERIA on galeve.GAL_ID_GALERIA equals img.GAL_ID_GALERIA
                          where even.EVEN_TIPO_EVENTO == false  &&
                                ((from par in db.PARTICIPANTE_EVENTO where par.EVEN_ID_EVENTO == even.EVEN_ID_EVENTO
                                  select par).Count() > 10)
                            select new
                            {
                                even.EVEN_ID_EVENTO,
                                even.EVEN_ID_CRIADOR_EVENTO,
                                even.EVEN_NOME_EVENTO,
                                img.GAL_BYTE_IMAGEM_GALERIA,
                                even.EVEN_DATA_EVENTO,
                                even.EVEN_CATEGORIA_EVENTO,
                                even.EVEN_DESCRICAO_EVENTO,
                                even.EVEN_TIPO_EVENTO,
                                even.EVEN_URL_EVENTO,
                                even.USUARIO.USUA_LOGIN_USUARIO,
                                  even.USUARIO.USUA_EMAIL_USUARIO,
                                  even.USUARIO.USUA_WHATSAPP_USUARIO,
                                even.ENDERECO.END_CIDADE_ENDERECO,
                                Quantidade = (from par in db.PARTICIPANTE_EVENTO
                                              where par.EVEN_ID_EVENTO == even.EVEN_ID_EVENTO
                                              select par).Count()
                            }).ToList().OrderByDescending(even => even.EVEN_DATA_EVENTO);

            if (eVENTO == null)
            {
                return NotFound();
            }

            return Ok(eVENTO);
        }

        // GET: api/EVENTOs
        [ResponseType(typeof(EVENTO))]
        public IHttpActionResult GetEVENTO(bool tipoEvento)
        {
            switch (tipoEvento) {

                case false:
                    var eVENTO = (from even in db.EVENTO
                                  join galeve in db.GALERIA_EVENTO on even.EVEN_ID_EVENTO equals galeve.EVEN_ID_EVENTO
                                  join img in db.GALERIA on galeve.GAL_ID_GALERIA equals img.GAL_ID_GALERIA
                                  where even.EVEN_TIPO_EVENTO == tipoEvento &&
                                  ((from par in db.PARTICIPANTE_EVENTO
                                    where par.EVEN_ID_EVENTO == even.EVEN_ID_EVENTO
                                    select par).Count() <= 10)
                                  select new {
                                      even.EVEN_ID_EVENTO,
                                      even.EVEN_ID_CRIADOR_EVENTO,
                                      even.EVEN_NOME_EVENTO,
                                      img.GAL_BYTE_IMAGEM_GALERIA,
                                      even.EVEN_DATA_EVENTO,
                                      even.EVEN_CATEGORIA_EVENTO,
                                      even.EVEN_DESCRICAO_EVENTO,
                                      even.EVEN_TIPO_EVENTO,
                                      even.EVEN_URL_EVENTO,
                                      even.USUARIO.USUA_LOGIN_USUARIO,
                                      even.USUARIO.USUA_EMAIL_USUARIO,
                                      even.USUARIO.USUA_WHATSAPP_USUARIO,
                                      even.ENDERECO.END_CIDADE_ENDERECO,
                                      Quantidade = (from par in db.PARTICIPANTE_EVENTO
                                                    where par.EVEN_ID_EVENTO == even.EVEN_ID_EVENTO
                                                    select par).Count()
                                  }).ToList().OrderByDescending(even => even.EVEN_DATA_EVENTO);

                    if (eVENTO == null) {
                        return NotFound();
                    }

                    return Ok(eVENTO);

                case true:
                    var eVENTOpriv = (from even in db.EVENTO
                                  join galeve in db.GALERIA_EVENTO on even.EVEN_ID_EVENTO equals galeve.EVEN_ID_EVENTO
                                  join img in db.GALERIA on galeve.GAL_ID_GALERIA equals img.GAL_ID_GALERIA
                                  where even.EVEN_TIPO_EVENTO == tipoEvento
                                  select new {
                                      even.EVEN_ID_EVENTO,
                                      even.EVEN_ID_CRIADOR_EVENTO,
                                      even.EVEN_NOME_EVENTO,
                                      img.GAL_BYTE_IMAGEM_GALERIA,
                                      even.EVEN_DATA_EVENTO,
                                      even.EVEN_CATEGORIA_EVENTO,
                                      even.EVEN_DESCRICAO_EVENTO,
                                      even.EVEN_TIPO_EVENTO,
                                      even.EVEN_URL_EVENTO,
                                      even.USUARIO.USUA_LOGIN_USUARIO,
                                      even.USUARIO.USUA_EMAIL_USUARIO,
                                      even.USUARIO.USUA_WHATSAPP_USUARIO,
                                      even.ENDERECO.END_CIDADE_ENDERECO,
                                      Quantidade = (from par in db.PARTICIPANTE_EVENTO
                                                    where par.EVEN_ID_EVENTO == even.EVEN_ID_EVENTO
                                                    select par).Count()
                                  }).ToList().OrderByDescending(even => even.EVEN_DATA_EVENTO);

                    if (eVENTOpriv == null) {
                        return NotFound();
                    }

                    return Ok(eVENTOpriv);

                default:
                    return null;
            }
            
        }

        // GET: api/EVENTOs/5
        [ResponseType(typeof(EVENTO))]
        public IHttpActionResult GetEVENTO(long id)
        {
            //EVENTO eVENTO = db.EVENTO.Find(id);
            var eVENTO = (from even in db.EVENTO
                          join galeve in db.GALERIA_EVENTO on even.EVEN_ID_EVENTO equals galeve.EVEN_ID_EVENTO
                          join img in db.GALERIA on galeve.GAL_ID_GALERIA equals img.GAL_ID_GALERIA
                          where even.EVEN_ID_EVENTO == id
                          select new {
                              even.EVEN_ID_EVENTO,
                              even.EVEN_ID_CRIADOR_EVENTO,
                              even.EVEN_NOME_EVENTO,
                              img.GAL_BYTE_IMAGEM_GALERIA,
                              even.EVEN_DATA_EVENTO,
                              even.EVEN_CATEGORIA_EVENTO,
                              even.EVEN_DESCRICAO_EVENTO,
                              even.EVEN_TIPO_EVENTO,
                              even.EVEN_URL_EVENTO,
                              even.USUARIO.USUA_LOGIN_USUARIO,
                              even.USUARIO.USUA_EMAIL_USUARIO,
                              even.USUARIO.USUA_WHATSAPP_USUARIO,
                              even.ENDERECO.END_CIDADE_ENDERECO,
                              even.ENDERECO.END_CEP_ENDERECO,
                              Quantidade = (from par in db.PARTICIPANTE_EVENTO
                                            where par.EVEN_ID_EVENTO == even.EVEN_ID_EVENTO
                                            select par).Count()
                          }).ToList().OrderByDescending(even => even.EVEN_DATA_EVENTO);

            if (eVENTO == null)
            {
                return NotFound();
            }

            return Ok(eVENTO);
        }

        //Eventos publicados pelo usuário recebido no parametro id
        [ResponseType(typeof(EVENTO))]
        public IHttpActionResult GetEVENTO(long idUsuario, string relatoriosEventos)
        {
            switch (relatoriosEventos)
            {
                case "Publicados":
                    var eVENTO = (from even in db.EVENTO
                                  join galeve in db.GALERIA_EVENTO on even.EVEN_ID_EVENTO equals galeve.EVEN_ID_EVENTO
                                  join img in db.GALERIA on galeve.GAL_ID_GALERIA equals img.GAL_ID_GALERIA
                                  where even.EVEN_ID_CRIADOR_EVENTO == idUsuario
                                  select new
                                  {
                                      even.EVEN_ID_EVENTO,
                                      even.EVEN_ID_CRIADOR_EVENTO,
                                      even.EVEN_NOME_EVENTO,
                                      img.GAL_BYTE_IMAGEM_GALERIA,
                                      even.EVEN_DATA_EVENTO,
                                      even.EVEN_CATEGORIA_EVENTO,
                                      even.EVEN_DESCRICAO_EVENTO,
                                      even.EVEN_TIPO_EVENTO,
                                      even.EVEN_URL_EVENTO,
                                      even.USUARIO.USUA_LOGIN_USUARIO,
                                      even.USUARIO.USUA_EMAIL_USUARIO,
                                      even.USUARIO.USUA_WHATSAPP_USUARIO,
                                      even.ENDERECO.END_CIDADE_ENDERECO,
                                      Quantidade = (from par in db.PARTICIPANTE_EVENTO
                                                    where par.EVEN_ID_EVENTO == even.EVEN_ID_EVENTO
                                                    select par).Count()
                                  }).ToList().OrderByDescending(even => even.EVEN_DATA_EVENTO);

                    if (eVENTO == null)
                    {
                        return NotFound();
                    }

                    return Ok(eVENTO);
                    

                case "Inscritos":
                    var eVENTOInscrito = (from even in db.EVENTO
                          join galeve in db.GALERIA_EVENTO on even.EVEN_ID_EVENTO equals galeve.EVEN_ID_EVENTO
                          join img in db.GALERIA on galeve.GAL_ID_GALERIA equals img.GAL_ID_GALERIA
                          join part in db.PARTICIPANTE_EVENTO on even.EVEN_ID_EVENTO equals part.EVEN_ID_EVENTO
                          where part.USUA_ID_USUARIO == idUsuario
                          select new
                          {
                              even.EVEN_ID_EVENTO,
                              even.EVEN_ID_CRIADOR_EVENTO,
                              even.EVEN_NOME_EVENTO,
                              img.GAL_BYTE_IMAGEM_GALERIA,
                              even.EVEN_DATA_EVENTO,
                              even.EVEN_CATEGORIA_EVENTO,
                              even.EVEN_DESCRICAO_EVENTO,
                              even.EVEN_TIPO_EVENTO,
                              even.EVEN_URL_EVENTO,
                              even.USUARIO.USUA_LOGIN_USUARIO,
                              even.USUARIO.USUA_EMAIL_USUARIO,
                              even.USUARIO.USUA_WHATSAPP_USUARIO,
                              even.ENDERECO.END_CIDADE_ENDERECO,
                              Quantidade = (from par in db.PARTICIPANTE_EVENTO
                                            where par.EVEN_ID_EVENTO == even.EVEN_ID_EVENTO
                                            select par).Count()
                          }).ToList().OrderByDescending(even => even.EVEN_DATA_EVENTO);


                    if (eVENTOInscrito == null)
                    {
                        return NotFound();
                    }

                    return Ok(eVENTOInscrito);

                default:
                    return null;
            }

            //EVENTO eVENTO = db.EVENTO.Find(id);
            

            //if (eVENTO == null)
            //{
            //    return NotFound();
            //}

            //return Ok(eVENTO);
        }

        // PUT: api/EVENTOs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEVENTO(long id, EVENTO eVENTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != eVENTO.EVEN_ID_EVENTO)
            {
                return BadRequest();
            }

            db.Entry(eVENTO).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EVENTOExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/EVENTOs
        [ResponseType(typeof(EVENTO))]
        public IHttpActionResult PostEVENTO(EVENTO eVENTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EVENTO.Add(eVENTO);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = eVENTO.EVEN_ID_EVENTO }, eVENTO);
        }

        // DELETE: api/EVENTOs/5
        [ResponseType(typeof(EVENTO))]
        public IHttpActionResult DeleteEVENTO(long id)
        {
            EVENTO eVENTO = db.EVENTO.Find(id);
            if (eVENTO == null)
            {
                return NotFound();
            }

            db.EVENTO.Remove(eVENTO);
            db.SaveChanges();

            return Ok(eVENTO);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EVENTOExists(long id)
        {
            return db.EVENTO.Count(e => e.EVEN_ID_EVENTO == id) > 0;
        }
    }
}