
namespace MyVote.Web.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using MyVote.Web.Data;
    using MyVote.Web.Data.Entities;
    using MyVote.Web.Data.Repositories;
    using MyVote.Web.Helpers;
    

    public class VotingEventsController : Controller
    {
        private readonly IVotingEventRepository votingEventRepository;
        private readonly IUserHelper userHelper;

        public VotingEventsController(IVotingEventRepository votingEventRepository, IUserHelper userHelper)
        {
            this.votingEventRepository = votingEventRepository;
            this.userHelper = userHelper;
        }


        // GET: Products
        public IActionResult Index()
        {
            return View(this.votingEventRepository.GetAll());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
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

            var product = await this.votingEventRepository.GetByIdAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
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
