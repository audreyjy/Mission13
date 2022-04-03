using Microsoft.AspNetCore.Mvc;
using Mission13.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission13.Components
{
    public class TeamsViewComponent : ViewComponent
    {
        // get data for component here 
        private BowlingDbContext _context { get; set; }

        //Contructor
        public TeamsViewComponent(BowlingDbContext temp)
        {
            _context = temp;
        }


        // invoke method 
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedTeam = RouteData.Values["bowlerTeam"];

            // get category types 
            var teams = _context.Bowlers
                .Select(x => x.Team.TeamName)
                .Distinct()
                .OrderBy(x => x);

            return View(teams);
        }
    }
}
