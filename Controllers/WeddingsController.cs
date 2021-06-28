using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeddingPlanner.Models;

namespace WeddingPlanner.Controllers
{
    public  class WeddingsController : Controller
    {
     
        private int? uid
        {
            get
            {
                return HttpContext.Session.GetInt32("UserId");
            }
        }

        private bool isLoggedIn
        {
            get
            {
                return uid != null;
            }
        }
        private WeddingPlannerContext db;
        public WeddingsController(WeddingPlannerContext context)
        {
            db = context;
        }


        //localhost5000/Dashboard
        [HttpGet("/dashboard")]
        public IActionResult Dashboard()
        {
            if(!isLoggedIn)
            {
                return RedirectToAction("Index", "Home");
            }

                /* List<Wedding> allWeddings = db.weddings
                .Include(w => w.Planner) //planner fromwedding model
                .Include(p => p.Attendees) //form wedding model list of association name
                .ThenInclude(a => a.User)
                .Where(w  => w.Date >= DateTime.Now)
                .OrderBy(w => w.Date)
                .ToList(); */

                List<Wedding> allWeddings = db.weddings
                    .Include(w  => w.Attendees)
                    .Include(a => a.Planner)
                    .ToList();
 

            return View ("Dashboard",allWeddings);
        }

        [HttpGet("/new/wedding")]
        public IActionResult NewWedding()
        {
            if(!isLoggedIn)
            {
                return RedirectToAction("Index", "Home");
            }

  
            return View("NewWedding");
        }

        [HttpPost("/create/wedding")]
        public IActionResult CreateWedding(Wedding newWedding)
        {
            if(!isLoggedIn)
            {
                return RedirectToAction("Index", "Home");
            } 

            if(ModelState.IsValid == false)
            {
                // Send back to the page with the form to show errors.
                return View("NewWedding");
            }
            //Validate date????
            // ModelState IS valid...
            newWedding.UserId = (int)uid;
            db.weddings.Add(newWedding);
            db.SaveChanges();
            return RedirectToAction("Dashboard");
            
        }

        [HttpGet("/weddings/{weddingId}")]
        public IActionResult Details(int weddingId)
        {
            if (!isLoggedIn)
            {
                return RedirectToAction("Index", "Home");
            }

            Wedding wedding = db.weddings
                .Include(w => w.Planner)
                .Include(p => p.Attendees)
                .ThenInclude(a => a.User)
                .FirstOrDefault(w => w.WeddingId == weddingId);

            if (wedding == null)
            {
                return RedirectToAction("Dashboard");
            }

            return View("Details",wedding);
        }

        [HttpGet("/weddings/{weddingID}/leave")]
        public IActionResult UnRSVP(int weddingID)
        {
            
            if (!isLoggedIn)
            {
                return RedirectToAction("Index");
            }
            Rsvp unrsvp = db.Rsvps
                .FirstOrDefault(r => r.WeddingId == weddingID && r.UserId == uid);
            db.Rsvps.Remove(unrsvp);
            db.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        [HttpGet("/weddings/{weddingID}/rsvp")]
        public IActionResult RSVP(int weddingID)
        {
            
            if (!isLoggedIn)
            {
                return RedirectToAction("Index");
            }
            Rsvp rsvp = new Rsvp();
            rsvp.WeddingId = weddingID;
            rsvp.UserId = (int)uid;
            db.Add(rsvp);
            db.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        [HttpGet("/weddings/{weddingID}/delete")]
        public IActionResult Delete(int weddingID)
        {
            Wedding todelete = db.weddings
                .FirstOrDefault(w => w.WeddingId == weddingID);
                
            if (!isLoggedIn)
            {
                return RedirectToAction("Index");
            }
            
                db.weddings.Remove(todelete);
                db.SaveChanges();
            return RedirectToAction("Dashboard");
        }
    }
}