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
    public class GALERIA_EVENTOController : ApiController
    {
        private EventopEntities db = new EventopEntities();

        // GET: api/GALERIA_EVENTO
        public IQueryable<GALERIA_EVENTO> GetGALERIA_EVENTO()
        {
            return db.GALERIA_EVENTO;
        }

        // GET: api/GALERIA_EVENTO/5
        [ResponseType(typeof(GALERIA_EVENTO))]
        public IHttpActionResult GetGALERIA_EVENTO(long id)
        {
            GALERIA_EVENTO gALERIA_EVENTO = db.GALERIA_EVENTO.Find(id);
            if (gALERIA_EVENTO == null)
            {
                return NotFound();
            }

            return Ok(gALERIA_EVENTO);
        }

        // PUT: api/GALERIA_EVENTO/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGALERIA_EVENTO(long id, GALERIA_EVENTO gALERIA_EVENTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != gALERIA_EVENTO.GALEVEN_ID_GALERIA_EVENTO)
            {
                return BadRequest();
            }

            db.Entry(gALERIA_EVENTO).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GALERIA_EVENTOExists(id))
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

        // POST: api/GALERIA_EVENTO
        [ResponseType(typeof(GALERIA_EVENTO))]
        public IHttpActionResult PostGALERIA_EVENTO(GALERIA_EVENTO gALERIA_EVENTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.GALERIA_EVENTO.Add(gALERIA_EVENTO);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = gALERIA_EVENTO.GALEVEN_ID_GALERIA_EVENTO }, gALERIA_EVENTO);
        }

        // DELETE: api/GALERIA_EVENTO/5
        [ResponseType(typeof(GALERIA_EVENTO))]
        public IHttpActionResult DeleteGALERIA_EVENTO(long id)
        {
            GALERIA_EVENTO gALERIA_EVENTO = db.GALERIA_EVENTO.Find(id);
            if (gALERIA_EVENTO == null)
            {
                return NotFound();
            }

            db.GALERIA_EVENTO.Remove(gALERIA_EVENTO);
            db.SaveChanges();

            return Ok(gALERIA_EVENTO);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GALERIA_EVENTOExists(long id)
        {
            return db.GALERIA_EVENTO.Count(e => e.GALEVEN_ID_GALERIA_EVENTO == id) > 0;
        }
    }
}