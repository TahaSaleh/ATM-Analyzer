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
    public class UserController : ApiController
    {
        private ATMEntities db = new ATMEntities();

        // GET: api/User
        public IQueryable<User> GetUsers()
        {
            return db.Users;
        }

        // GET: api/User/5
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [Route("api/user/getuser/{user_name?}")]
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(string user_name)
        {
            User user = db.Users.FirstOrDefault(i => i.user_name == user_name);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [Route("api/user/credite/{credit_number?}")]
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUserwithcredit(string credit_number)
        {
            User user = db.Users.FirstOrDefault(i => i.credit_number == credit_number);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
        //check for login true or false
        [HttpGet]
        [Route("api/login/{credit_number?}")]
        public IHttpActionResult login(string credit_number)
        {
            
            User x = db.Users.FirstOrDefault(i => i.credit_number == credit_number);
            if (x != null)
            {
                    return Content(HttpStatusCode.OK,"true");
               
            }
            else
                return Content(HttpStatusCode.NotFound, "false");
        }
        // PUT: api/User/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.id)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/User
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            List<User> x = db.Users.Where(i => i.credit_number == user.credit_number).ToList();
            if (x.Count !=0)
            {
                return Content(HttpStatusCode.BadRequest,"false");
            }
            db.Users.Add(user);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = user.id }, user);
        }

        // DELETE: api/User/5
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            db.SaveChanges();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.id == id) > 0;
        }
    }
}