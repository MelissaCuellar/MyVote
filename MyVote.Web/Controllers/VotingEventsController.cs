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
        private readonly IRepository repository;

        public VotingEventsController(IRepository repository)
        {
            this.repository = repository;
        }

        
        public IActionResult Index()
        {
            return View(this.repository.GetVotingEvents());
        }

       
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var votingEvent = this.repository.GetVotingEvent(id.Value);
            if (votingEvent == null)
            {
                return NotFound();
            }

            return View(votingEvent);
        }

        
        public IActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VotingEvent votingEvent)
        {
            if (ModelState.IsValid)
            {
                this.repository.AddVotingEvent(votingEvent);
                await this.repository.SaveAllAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(votingEvent);
        }

        // GET: VotingEvents/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var votingEvent = this.repository.GetVotingEvent(id.Value);
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
        public async Task<IActionResult> Edit(VotingEvent votingEvent)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    this.repository.UpdateVotingEvent(votingEvent);
                    await this.repository.SaveAllAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.repository.VotingEventExists(votingEvent.Id))
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
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var votingEvent = this.repository.GetVotingEvent(id.Value);
            if (votingEvent == null)
            {
                return NotFound();
            }

            return View(votingEvent);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var votingEvent = this.repository.GetVotingEvent(id);
            this.repository.RemoveVotingEvent(votingEvent);
            await this.repository.SaveAllAsync();
            return RedirectToAction(nameof(Index));
        }

       
    }
}
