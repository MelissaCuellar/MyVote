using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyVote.Web.Data;
using MyVote.Web.Data.Entities;

namespace MyVote.Web.Controllers
{
    public class VotingEventsController : Controller
    {
        private readonly DataContext _context;

        public VotingEventsController(DataContext context)
        {
            _context = context;
        }

        // GET: VotingEvents
        public async Task<IActionResult> Index()
        {
            return View(await _context.VotingEvents.ToListAsync());
        }

        // GET: VotingEvents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var votingEvent = await _context.VotingEvents
                .FirstOrDefaultAsync(m => m.Id == id);
            if (votingEvent == null)
            {
                return NotFound();
            }

            return View(votingEvent);
        }

        // GET: VotingEvents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VotingEvents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,StartDate,EndDate")] VotingEvent votingEvent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(votingEvent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(votingEvent);
        }

        // GET: VotingEvents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var votingEvent = await _context.VotingEvents.FindAsync(id);
            if (votingEvent == null)
            {
                return NotFound();
            }
            return View(votingEvent);
        }

        // POST: VotingEvents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,StartDate,EndDate")] VotingEvent votingEvent)
        {
            if (id != votingEvent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(votingEvent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VotingEventExists(votingEvent.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(votingEvent);
        }

        // GET: VotingEvents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var votingEvent = await _context.VotingEvents
                .FirstOrDefaultAsync(m => m.Id == id);
            if (votingEvent == null)
            {
                return NotFound();
            }

            return View(votingEvent);
        }

        // POST: VotingEvents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var votingEvent = await _context.VotingEvents.FindAsync(id);
            _context.VotingEvents.Remove(votingEvent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VotingEventExists(int id)
        {
            return _context.VotingEvents.Any(e => e.Id == id);
        }
    }
}
