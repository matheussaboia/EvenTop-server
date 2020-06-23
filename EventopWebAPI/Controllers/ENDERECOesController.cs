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
    public class ENDERECOesController : ApiController
    {
        private EventopEntities db = new EventopEntities();

        // GET: api/ENDERECOes
        public IQueryable<ENDERECO> GetENDERECO()
        {
            return db.ENDERECO;
        }

        // GET: api/ENDERECOes/5
        [ResponseType(typeof(ENDERECO))]
        public IHttpActionResult GetENDERECO(long id)
        {
            ENDERECO eNDERECO = db.ENDERECO.Find(id);
            if (eNDERECO == null)
            {
                return NotFound();
            }

            return Ok(eNDERECO);
        }

        // PUT: api/ENDERECOes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutENDERECO(long id, ENDERECO eNDERECO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != eNDERECO.END_ID_ENDERECO)
            {
                return BadRequest();
            }

            db.Entry(eNDERECO).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ENDERECOExists(id))
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

        // POST: api/ENDERECOes
        [ResponseType(typeof(ENDERECO))]
        public IHttpActionResult PostENDERECO(ENDERECO eNDERECO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ENDERECO.Add(eNDERECO);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = eNDERECO.END_ID_ENDERECO }, eNDERECO);
        }

        // DELETE: api/ENDERECOes/5
        [ResponseType(typeof(ENDERECO))]
        public IHttpActionResult DeleteENDERECO(long id)
        {
            ENDERECO eNDERECO = db.ENDERECO.Find(id);
            if (eNDERECO == null)
            {
                return NotFound();
            }

            db.ENDERECO.Remove(eNDERECO);
            db.SaveChanges();

            return Ok(eNDERECO);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ENDERECOExists(long id)
        {
            return db.ENDERECO.Count(e => e.END_ID_ENDERECO == id) > 0;
        }
    }
}