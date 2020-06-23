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
    public class USUARIOsController : ApiController
    {
        private EventopEntities db = new EventopEntities();

        // GET: api/USUARIOs
        public IQueryable<USUARIO> GetUSUARIO()
        {
            return db.USUARIO;
        }

        // GET: api/USUARIOs/5
        [ResponseType(typeof(USUARIO))]
        public IHttpActionResult GetUSUARIO(long id)
        {
            var uSUARIO = from usu in db.USUARIO
                          where usu.USUA_ID_USUARIO == id
                          select usu;
            if (uSUARIO == null)
            {
                return NotFound();
            }

            return Ok(uSUARIO);
        }

        // GET: api/USUARIOs/5
        [ResponseType(typeof(USUARIO))]
        public IHttpActionResult GetUSUARIO(long id, bool criador)
        {
            var uSUARIO = from usu in db.USUARIO
                          join eve in db.EVENTO on usu.USUA_ID_USUARIO equals eve.EVEN_ID_CRIADOR_EVENTO
                          where usu.USUA_ID_USUARIO == id
                          select usu;
            if (uSUARIO == null)
            {
                return NotFound();
            }

            return Ok(uSUARIO);
        }

        /*[ResponseType(typeof(USUARIO))]
        public IHttpActionResult GetUSUARIO()
        {
            var uSUARIO = from usuario in db.USUARIO
                          select usuario;

            if (uSUARIO == null)
            {
                return NotFound();
            }

            return Ok(uSUARIO);
        }
        */

        // GET: api/USUARIOs/5
        [ResponseType(typeof(USUARIO))]
        public IHttpActionResult GetUSUARIO(string usuario, string senha)
        {
            var uSUARIO = from a in db.USUARIO
                          where (a.USUA_LOGIN_USUARIO == usuario || a.USUA_DOCUMENTO_USUARIO == usuario)
                                && (a.USUA_SENHA_USUARIO == senha)
                          select a;

            if (uSUARIO == null)
            {
                return NotFound();
            }

            return Ok(uSUARIO);
        }

        // GET: api/USUARIOs/5
        [ResponseType(typeof(USUARIO))]
        public IHttpActionResult GetUSUARIO(string usuario, string documento, bool existeConta) {
            var uSUARIO = from a in db.USUARIO
                          where a.USUA_LOGIN_USUARIO == usuario || a.USUA_DOCUMENTO_USUARIO == documento
                          select a;

            if (uSUARIO == null) {
                return NotFound();
            }

            return Ok(uSUARIO);
        }

        // PUT: api/USUARIOs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUSUARIO(long id, USUARIO uSUARIO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != uSUARIO.USUA_ID_USUARIO)
            {
                return BadRequest();
            }

            db.Entry(uSUARIO).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!USUARIOExists(id))
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

        // POST: api/USUARIOs
        [ResponseType(typeof(USUARIO))]
        public IHttpActionResult PostUSUARIO(USUARIO uSUARIO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.USUARIO.Add(uSUARIO);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = uSUARIO.USUA_ID_USUARIO }, uSUARIO);
        }

        // DELETE: api/USUARIOs/5
        [ResponseType(typeof(USUARIO))]
        public IHttpActionResult DeleteUSUARIO(long id)
        {
            USUARIO uSUARIO = db.USUARIO.Find(id);
            if (uSUARIO == null)
            {
                return NotFound();
            }

            db.USUARIO.Remove(uSUARIO);
            db.SaveChanges();

            return Ok(uSUARIO);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool USUARIOExists(long id)
        {
            return db.USUARIO.Count(e => e.USUA_ID_USUARIO == id) > 0;
        }
    }
}