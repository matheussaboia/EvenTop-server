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
    public class PARTICIPANTE_EVENTOController : ApiController
    {
        private EventopEntities db = new EventopEntities();

        // GET: api/PARTICIPANTE_EVENTO
        //public IQueryable<PARTICIPANTE_EVENTO> GetPARTICIPANTE_EVENTO()
        //{
        //    return db.PARTICIPANTE_EVENTO;
        //}

        // GET: api/PARTICIPANTE_EVENTO/5
        [ResponseType(typeof(PARTICIPANTE_EVENTO))]
        public IHttpActionResult GetPARTICIPANTE_EVENTO(long id)
        {
            //PARTICIPANTE_EVENTO pARTICIPANTE_EVENTO = db.PARTICIPANTE_EVENTO.Find(id);
            var pARTICIPANTE_EVENTO = from part in db.PARTICIPANTE_EVENTO
                                      join usu in db.USUARIO on part.USUA_ID_USUARIO equals usu.USUA_ID_USUARIO
                                      where part.EVEN_ID_EVENTO == id
                                      orderby part.PAR_DATA_PARTICIPANTE descending
                                      select new
                                      {
                                          usu.USUA_LOGIN_USUARIO,
                                          usu.USUA_EMAIL_USUARIO,
                                          usu.USUA_WHATSAPP_USUARIO,
                                          usu.USUA_SEXO_USUARIO,
                                          part.PAR_DATA_PARTICIPANTE
                                      };

            if (pARTICIPANTE_EVENTO == null)
            {
                return NotFound();
            }

            return Ok(pARTICIPANTE_EVENTO);
        }

        // GET: api/PARTICIPANTE_EVENTO/5
        [ResponseType(typeof(PARTICIPANTE_EVENTO))]
        public IHttpActionResult GetPARTICIPANTE_EVENTO(long idEvento, long idParticipante)
        {
            var pARTICIPANTE_EVENTO = from part in db.PARTICIPANTE_EVENTO
                                      where part.USUA_ID_USUARIO == idParticipante && part.EVEN_ID_EVENTO == idEvento
                                      select part;
            if (pARTICIPANTE_EVENTO == null)
            {
                return NotFound();
            }

            return Ok(pARTICIPANTE_EVENTO);
        }
        [ResponseType(typeof(PARTICIPANTE_EVENTO))]
        public IHttpActionResult GetPARTICIPANTE_EVENTO(long id, bool temComentario)
        {
            //PARTICIPANTE_EVENTO pARTICIPANTE_EVENTO = db.PARTICIPANTE_EVENTO.Find(id);
            var pARTICIPANTE_EVENTO = from part in db.PARTICIPANTE_EVENTO
                                      join usu in db.USUARIO on part.USUA_ID_USUARIO equals usu.USUA_ID_USUARIO
                                      where part.EVEN_ID_EVENTO == id && part.PAR_DESCRICAO_AVALIACAO != null
                                      orderby part.PAR_DATA_AVALIACAO descending
                                      select new
                                      {
                                          part.USUARIO.USUA_LOGIN_USUARIO,
                                          part.PAR_DATA_AVALIACAO,
                                          part.PAR_DESCRICAO_AVALIACAO
                                      };

            if (pARTICIPANTE_EVENTO == null)
            {
                return NotFound();
            }

            return Ok(pARTICIPANTE_EVENTO);
        }



        // PUT: api/PARTICIPANTE_EVENTO/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPARTICIPANTE_EVENTO(long idDoEvento, PARTICIPANTE_EVENTO participante)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (idDoEvento != participante.PAR_ID_PARTICIPANTE)
            {
                return BadRequest();
            }

            db.Entry(participante).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PARTICIPANTE_EVENTOExists(idDoEvento))
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

        // POST: api/PARTICIPANTE_EVENTO
        [ResponseType(typeof(PARTICIPANTE_EVENTO))]
        public IHttpActionResult PostPARTICIPANTE_EVENTO(PARTICIPANTE_EVENTO pARTICIPANTE_EVENTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PARTICIPANTE_EVENTO.Add(pARTICIPANTE_EVENTO);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = pARTICIPANTE_EVENTO.PAR_ID_PARTICIPANTE }, pARTICIPANTE_EVENTO);
        }

        // DELETE: api/PARTICIPANTE_EVENTO/5
        [ResponseType(typeof(PARTICIPANTE_EVENTO))]
        public IHttpActionResult DeletePARTICIPANTE_EVENTO(long id)
        {
            PARTICIPANTE_EVENTO pARTICIPANTE_EVENTO = db.PARTICIPANTE_EVENTO.Find(id);
            if (pARTICIPANTE_EVENTO == null)
            {
                return NotFound();
            }

            db.PARTICIPANTE_EVENTO.Remove(pARTICIPANTE_EVENTO);
            db.SaveChanges();

            return Ok(pARTICIPANTE_EVENTO);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PARTICIPANTE_EVENTOExists(long id)
        {
            return db.PARTICIPANTE_EVENTO.Count(e => e.PAR_ID_PARTICIPANTE == id) > 0;
        }
    }
}