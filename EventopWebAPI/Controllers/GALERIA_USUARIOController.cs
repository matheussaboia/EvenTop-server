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
    public class GALERIA_USUARIOController : ApiController
    {
        private EventopEntities db = new EventopEntities();

        // GET: api/GALERIA_USUARIO
        public IQueryable<GALERIA_USUARIO> GetGALERIA_USUARIO()
        {
            return db.GALERIA_USUARIO;
        }

        // GET: api/GALERIA_USUARIO/5
        [ResponseType(typeof(GALERIA_USUARIO))]
        public IHttpActionResult GetGALERIA_USUARIO(long id)
        {
            var gALERIA_USUARIO = (from galeve in db.GALERIA_USUARIO
                                   join gal in db.GALERIA on galeve.GAL_ID_GALERIA equals gal.GAL_ID_GALERIA
                                   where galeve.USUA_ID_USUARIO == id
                                   select gal).OrderByDescending(o=>o.GAL_ID_GALERIA);
            if (gALERIA_USUARIO == null)
            {
                return NotFound();
            }

            return Ok(gALERIA_USUARIO);
        }

        // PUT: api/GALERIA_USUARIO/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGALERIA_USUARIO(long id, GALERIA_USUARIO gALERIA_USUARIO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != gALERIA_USUARIO.GALEVEN_ID_GALERIA_USUARIO)
            {
                return BadRequest();
            }

            db.Entry(gALERIA_USUARIO).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GALERIA_USUARIOExists(id))
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

        // POST: api/GALERIA_USUARIO
        [ResponseType(typeof(GALERIA_USUARIO))]
        public IHttpActionResult PostGALERIA_USUARIO(GALERIA_USUARIO gALERIA_USUARIO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.GALERIA_USUARIO.Add(gALERIA_USUARIO);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = gALERIA_USUARIO.GALEVEN_ID_GALERIA_USUARIO }, gALERIA_USUARIO);
        }

        // DELETE: api/GALERIA_USUARIO/5
        [ResponseType(typeof(GALERIA_USUARIO))]
        public IHttpActionResult DeleteGALERIA_USUARIO(long id)
        {
            GALERIA_USUARIO gALERIA_USUARIO = db.GALERIA_USUARIO.Find(id);
            if (gALERIA_USUARIO == null)
            {
                return NotFound();
            }

            db.GALERIA_USUARIO.Remove(gALERIA_USUARIO);
            db.SaveChanges();

            return Ok(gALERIA_USUARIO);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GALERIA_USUARIOExists(long id)
        {
            return db.GALERIA_USUARIO.Count(e => e.GALEVEN_ID_GALERIA_USUARIO == id) > 0;
        }
    }
}