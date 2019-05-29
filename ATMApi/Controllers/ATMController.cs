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
using ATMApi.Models;

namespace ATMApi.Controllers
{
    [Authorize]
    public class ATMController : ApiController
    {
        private ATMEntities db = new ATMEntities();
        // GET: api/ATM
        public IQueryable<ATM> GetATMs()
        {
            return db.ATMs;
        }

        // GET: api/ATM/5
        [ResponseType(typeof(ATM))]
        public IHttpActionResult GetATM(int id)
        {
            DateTime d1, d2;
            d1 = DateTime.Now;
            d2 = d1.AddHours(-1);
            Models.ATM aTM = db.ATMs.Find(id);
            if (aTM == null)
            {
                return NotFound();
            }
            else
            {
                SimulationController.trunsaction_Between_date(id, d2, d1);
                aTM = db.ATMs.Find(id);
                return Ok(aTM);
            }
        }

        // PUT: api/ATM/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutATM(int id, ATM aTM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != aTM.id)
            {
                return BadRequest();
            }

            db.Entry(aTM).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ATMExists(id))
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

        // POST: api/ATM
        [ResponseType(typeof(ATM))]
        public IHttpActionResult PostATM(ATM aTM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ATMs.Add(aTM);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = aTM.id }, aTM);
        }

        // DELETE: api/ATM/5
        [ResponseType(typeof(ATM))]
        public IHttpActionResult DeleteATM(int id)
        {
            ATM aTM = db.ATMs.Find(id);
            if (aTM == null)
            {
                return NotFound();
            }

            db.ATMs.Remove(aTM);
            db.SaveChanges();

            return Ok(aTM);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ATMExists(int id)
        {
            return db.ATMs.Count(e => e.id == id) > 0;
        }
    }
}