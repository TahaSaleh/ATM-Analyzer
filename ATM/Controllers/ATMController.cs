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
    public class ATMController : ApiController
    {
        private ATMEntities db = new ATMEntities();
        //ATMEntities db1;
        // GET: api/ATM
        public List<Models.ATM> GetATMs()
        {
            
            return db.ATMs.ToList();
            //return db.ATMs;
        }

        // GET: api/ATM/5 
        [ResponseType(typeof(Models.ATM))]
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
                SimulationController.trunsaction_Between_date(id,d2,d1);
                aTM= db.ATMs.Find(id);
                return Ok(aTM);
            }
        }

        // PUT: api/ATM/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutATM(int id, Models.ATM aTM)
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
        [ResponseType(typeof(Models.ATM))]
        public IHttpActionResult PostATM(Models.ATM aTM)
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
        [ResponseType(typeof(Models.ATM))]
        public IHttpActionResult DeleteATM(int id)
        {
            Models.ATM aTM = db.ATMs.Find(id);
            if (aTM == null)
            {
                return NotFound();
            }

            db.ATMs.Remove(aTM);
            db.SaveChanges();

            return Ok(aTM);
        }


        //[Route("api/atm/gettrunsaction/{atm_id?}/{date_time?}")]
        //[ResponseType(typeof(Models.ATM))]
        //public IHttpActionResult gettrunsaction(int id,string date_time)
        //{
        //    db1 = new ATMEntities();
        //    int x;
        //    DateTime starttime = DateTime.Parse(date_time);
        //    DateTime endtime = starttime.AddHours(-1);
        //    List<Trunsaction> q = db.Trunsactions.Where(i => i.atm_id == id).ToList();
        //    List<Trunsaction> t = q.Where(i => i.start_time >= starttime && i.start_time < endtime).ToList();
        //    x = t.Count;
        //    var atm = db.ATMs.Where(i => i.id == id).FirstOrDefault();
        //    atm.T_N_last_hour = x;
        //    db.SaveChanges();
        //    Models.ATM atm1 = db.ATMs.Find(id);
        //    db1 = null;
        //    return Ok(atm1);
        //}
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