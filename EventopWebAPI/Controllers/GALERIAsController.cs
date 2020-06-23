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
    public class GALERIAsController : ApiController
    {
        private EventopEntities db = new EventopEntities();

        // GET: api/GALERIAs
        public IQueryable<GALERIA> GetGALERIA()
        {
            return db.GALERIA;
        }

        // GET: api/GALERIAs/5
        [ResponseType(typeof(GALERIA))]
        public IHttpActionResult GetGALERIA(long id)
        {
            GALERIA gALERIA = db.GALERIA.Find(id);
            if (gALERIA == null)
            {
                return NotFound();
            }

            return Ok(gALERIA);
        }

        // PUT: api/GALERIAs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGALERIA(long id, GALERIA gALERIA)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != gALERIA.GAL_ID_GALERIA)
            {
                return BadRequest();
            }

            db.Entry(gALERIA).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GALERIAExists(id))
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

        // POST: api/GALERIAs
        [ResponseType(typeof(GALERIA))]
        public IHttpActionResult PostGALERIA(GALERIA gALERIA)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.GALERIA.Add(gALERIA);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = gALERIA.GAL_ID_GALERIA }, gALERIA);
        }

        // DELETE: api/GALERIAs/5
        [ResponseType(typeof(GALERIA))]
        public IHttpActionResult DeleteGALERIA(long id)
        {
            GALERIA gALERIA = db.GALERIA.Find(id);
            if (gALERIA == null)
            {
                return NotFound();
            }

            db.GALERIA.Remove(gALERIA);
            db.SaveChanges();

            return Ok(gALERIA);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GALERIAExists(long id)
        {
            return db.GALERIA.Count(e => e.GAL_ID_GALERIA == id) > 0;
        }
    }
}