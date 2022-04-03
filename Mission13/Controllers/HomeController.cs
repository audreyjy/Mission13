using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; 
using Microsoft.Extensions.Logging;
using Mission13.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Mission13.Controllers
{
    public class HomeController : Controller
    {
        private BowlingDbContext _context { get; set; }


        // Constructor
        public HomeController(BowlingDbContext temp)
        {
            _context = temp; 
        }

        // INDEX 
        public IActionResult Index()
        {
            return View(); 
        }

        // GET LIST OF BOWLERS 
        [HttpGet]
        public IActionResult ListBowlers(string bowlerTeam)
        {

            if (bowlerTeam is null)
            {
                ViewBag.Header = "Home";
            }
            else
            {
                ViewBag.Header = bowlerTeam;
            }

            var bowlerTable = _context.Bowlers
                .Where(x => x.Team.TeamName == bowlerTeam || bowlerTeam == null) // Only get projects where they match the bowlerTeam selected or if none is selected show all 
                .Include(x => x.Team) //Inner join on Team model
                .OrderBy(x => x.BowlerFirstName)
                .ToList(); 

            return View(bowlerTable); 

        }

        // GET FORM
        [HttpGet]
        public IActionResult Form()
        {

            ViewBag.Team = _context.Teams.ToList(); 
            return View(); 
        }

        // POST FORM
        [HttpPost]
        public IActionResult Form(Bowler b)
        {

            ViewBag.Team = _context.Teams.ToList();
            _context.Add(b);
            _context.SaveChanges(); 


            return View("Confirmation", b); 
        }

        // GET EDIT
        [HttpGet]
        public IActionResult Edit(int bowlerid)
        {
            ViewBag.Team = _context.Teams.ToList();
            var x = _context.Bowlers.Single(b => b.BowlerID == bowlerid); 
            return View("Form", x); 
        }


        // POST EDIT
        [HttpPost]
        public IActionResult Edit(Bowler edited)
        {
            _context.Update(edited);
            _context.SaveChanges();

            return RedirectToAction("ListBowlers");

        }

        // GET DELETE 
        [HttpGet]
        public IActionResult Delete(int bowlerid)
        {
            var delete_bowler = _context.Bowlers.Single(b => b.BowlerID == bowlerid);
            return View(delete_bowler); 
        }

        [HttpPost]
        public IActionResult Delete(Bowler del)
        {
            _context.Bowlers.Remove(del);
            _context.SaveChanges();

            return RedirectToAction("ListBowlers"); 
        }


    }
}
