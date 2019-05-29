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
using ATM.Models;

namespace ATM.Controllers
{
    public class TrunsactionController : ApiController
    {
        private ATMEntities db = new ATMEntities();

        // GET: api/Trunsaction
        public List<Trunsaction> GetTrunsactions()
        {
            return db.Trunsactions.ToList();
        }

        // GET: api/Trunsaction/5
        [ResponseType(typeof(Trunsaction))]
        public IHttpActionResult GetTrunsaction(int id)
        {
            Trunsaction trunsaction = db.Trunsactions.Find(id);
            if (trunsaction == null)
            {
                return NotFound();
            }

            return Ok(trunsaction);
        }

        // PUT: api/Trunsaction/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTrunsaction(int id, Trunsaction trunsaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != trunsaction.id)
            {
                return BadRequest();
            }

            db.Entry(trunsaction).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrunsactionExists(id))
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

        // POST: api/Trunsaction
        [ResponseType(typeof(Trunsaction))]
        public IHttpActionResult PostTrunsaction(Trunsaction trunsaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Trunsactions.Add(trunsaction);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = trunsaction.id }, trunsaction);
        }

        // DELETE: api/Trunsaction/5
        [ResponseType(typeof(Trunsaction))]
        public IHttpActionResult DeleteTrunsaction(int id)
        {
            Trunsaction trunsaction = db.Trunsactions.Find(id);
            if (trunsaction == null)
            {
                return NotFound();
            }

            db.Trunsactions.Remove(trunsaction);
            db.SaveChanges();

            return Ok(trunsaction);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TrunsactionExists(int id)
        {
            return db.Trunsactions.Count(e => e.id == id) > 0;
        }
    }
}