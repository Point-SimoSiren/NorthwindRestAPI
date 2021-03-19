using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using NorthwindCore.Models;

namespace NorthwindCore.Controllers
{
    [Route("nw/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        // Esitellään context vain kerran tällä tasolla niin ei tarvitse metodissa tehdä sitä joka kerta.
        private northwindContext db = new northwindContext();

        // Kaikkien käyttäjien haku

        [HttpGet]
        [Route("")]
        public List<Logins> GetAllLogins()
        {
            try
            {
                List<Logins> logins = db.Logins.ToList();
                return logins;
            }
            finally
            {
                db.Dispose();
            }
        }

        // Haku id:llä
        [HttpGet]
        [Route("{id}")]
        public Logins GetOneLogin(int id)
        {
            try
            {
                Logins login = db.Logins.Find(id);
                return login;
            }
            finally
            {
                db.Dispose();
            }
        }


        // Uuden luonti
        [HttpPost]
        [Route("")]
        public ActionResult CreateNewLogin([FromBody] Logins login)
        {
            try
            {

                db.Logins.Add(login);
                db.SaveChanges();
                return Ok(login.LoginId);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            finally
            {
                db.Dispose();
            }

        }

 
        //Yksittäisen käyttäjän poistaminen
        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteLogin(int id)
        {
            try
            {
                Logins login = db.Logins.Find(id);
                if (login != null)
                {
                    db.Logins.Remove(login);
                    db.SaveChanges();
                    return Ok("Käyttäjä id:llä " + id + " poistettiin");
                }
                else
                {
                    return NotFound("Käyttäjää id:llä" + id + " ei löydy");
                }
            }
            catch
            {
                return BadRequest();
            }
            finally
            {
                db.Dispose();
            }
        }
    }
}