
namespace MyVote.Web.Controllers
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using MyVote.Web.Data;
    using MyVote.Web.Data.Entities;
    using MyVote.Web.Data.Repositories;
    using MyVote.Web.Helpers;
    using MyVote.Web.Models;

    [Authorize]
    public class VotingEventsController : Controller
    {
        private readonly IVotingEventRepository votingEventRepository;
        private readonly IUserHelper userHelper;

        public VotingEventsController(IVotingEventRepository votingEventRepository, IUserHelper userHelper)
        {
            this.votingEventRepository = votingEventRepository;
            this.userHelper = userHelper;
        }

        public async Task<IActionResult> DeleteCandidate(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidate = await this.votingEventRepository.GetCandidateAsync(id.Value);
            if (candidate == null)
            {
                return NotFound();
            }

            var votingEventId = await this.votingEventRepository.DeleteCandidateAsync(candidate);
            return this.RedirectToAction($"Details/{votingEventId}");
        }

        public async Task<IActionResult> EditCandidate(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidate = await this.votingEventRepository.GetCandidateAsync(id.Value);
            if (candidate == null)
            {
                return NotFound();
            }
            var view = this.ToCandidateViewModel(candidate);
            return View(view);
        }
        
        private CandidateViewModel ToCandidateViewModel(Candidate candidate)
        {
            return new CandidateViewModel
            {               
                CandidateId = candidate.Id,
                Name=candidate.Name,
                Proposal=candidate.Proposal,
                ImageUrl=candidate.ImageUrl
            };
        }

        [HttpPost]
        public async Task<IActionResult> EditCandidate(CandidateViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                try
                {                   
                        var path = model.ImageUrl;

                        if (model.ImageFile != null && model.ImageFile.Length > 0)
                        {
                            var guid = Guid.NewGuid().ToString();
                            var file = $"{guid}.jpg";

                            path = Path.Combine(
                                Directory.GetCurrentDirectory(),
                                "wwwroot\\images\\Candidates",
                                file);

                            using (var stream = new FileStream(path, FileMode.Create))
                            {
                                await model.ImageFile.CopyToAsync(stream);
                            }

                            path = $"~/images/Candidates/{file}";
                        }
                    var candidate = this.ToCandidate(model, path);
                    var EventId = await this.votingEventRepository.UpdateCandidateAsync(candidate);
                    if (EventId != 0)
                    {
                        return this.RedirectToAction($"Details/{EventId}");
                    }
                }
                catch
                {
                    if (!await this.votingEventRepository.ExistAsync(model.VotingEventId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
               
            }

            return this.View(model);
        }

        private Candidate ToCandidate(CandidateViewModel model, string path)
        {
            return new Candidate
            {
                Id=model.CandidateId,
                ImageUrl=path,
                Name=model.Name,
                Proposal=model.Proposal
                
            };
        }

        public async Task<IActionResult> AddCandidate(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var votingEvent = await this.votingEventRepository.GetByIdAsync(id.Value);
            if (votingEvent == null)
            {
                return NotFound();
            }

            var model = new CandidateViewModel { VotingEventId = votingEvent.Id };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddCandidate(CandidateViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var path = string.Empty;

                if (model.ImageFile != null && model.ImageFile.Length > 0)
                {
                    var guid = Guid.NewGuid().ToString();
                    var file = $"{guid}.jpg";

                    path = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot\\images\\Candidates",
                        file);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await model.ImageFile.CopyToAsync(stream);
                    }

                    path = $"~/images/Candidates/{file}";
                }

                await this.votingEventRepository.AddCandidateAsync(model, path);
                return this.RedirectToAction($"Details/{model.VotingEventId}");
            }

            return this.View(model);
        }

        // GET: Products
        public IActionResult Index()
        {
            return View(this.votingEventRepository.GetAll().OrderBy(v=>v.Name));
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var votingEvent = await this.votingEventRepository.GetVotingEventWithCandidatesAsync(id.Value);
            if (votingEvent == null)
            {
                return NotFound();
            }

            return View(votingEvent);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            var model = new VotingEvent
            {
                StartDate = DateTime.Today,
                EndDate=DateTime.Today
            };
            return View(model);
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VotingEvent votingEvent)
        {
            if (ModelState.IsValid)
            {
                // TODO: Pending to change to: this.User.Identity.Name
                votingEvent.User = await this.userHelper.GetUserByEmailAsync("meli.cuellar0117@gmail.com");
                await this.votingEventRepository.CreateAsync(votingEvent);
                return RedirectToAction(nameof(Index));
            }

            return View(votingEvent);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var votingEvent = await this.votingEventRepository.GetByIdAsync(id.Value);
            if (votingEvent == null)
            {
                return NotFound();
            }

            return View(votingEvent);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(VotingEvent votingEvent)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Pending to change to: this.User.Identity.Name
                    votingEvent.User = await this.userHelper.GetUserByEmailAsync("meli.cuellar0117@gmail.com");
                    await this.votingEventRepository.UpdateAsync(votingEvent);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await this.votingEventRepository.ExistAsync(votingEvent.Id))
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

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await this.votingEventRepository.GetByIdAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await this.votingEventRepository.GetByIdAsync(id);
            await this.votingEventRepository.DeleteAsync(product);
            return RedirectToAction(nameof(Index));
        }



    }
}
