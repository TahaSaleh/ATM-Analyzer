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
    public class BankController : ApiController
    {
        private ATMEntities db = new ATMEntities();

        // GET: api/Bank
        public List<Bank> GetBanks()
        {
            return db.Banks.ToList();
        }

        // GET: api/Bank/5
        [ResponseType(typeof(Bank))]
        public IHttpActionResult GetBank(int id)
        {
            Bank bank = db.Banks.Find(id);
            if (bank == null)
            {
                return NotFound();
            }

            return Ok(bank);
        }

        // PUT: api/Bank/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBank(int id, Bank bank)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bank.id)
            {
                return BadRequest();
            }

            db.Entry(bank).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BankExists(id))
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

        // POST: api/Bank
        [ResponseType(typeof(Bank))]
        public IHttpActionResult PostBank(Bank bank)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Banks.Add(bank);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = bank.id }, bank);
        }

        // DELETE: api/Bank/5
        [ResponseType(typeof(Bank))]
        public IHttpActionResult DeleteBank(int id)
        {
            Bank bank = db.Banks.Find(id);
            if (bank == null)
            {
                return NotFound();
            }

            db.Banks.Remove(bank);
            db.SaveChanges();

            return Ok(bank);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BankExists(int id)
        {
            return db.Banks.Count(e => e.id == id) > 0;
        }
    }
}