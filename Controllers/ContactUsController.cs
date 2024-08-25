using ECommerceWebsite.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWebsite.Controllers
{
    public class ContactUsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContactUsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ContactUs
        public async Task<IActionResult> Index()
        {
            ViewBag.name = HttpContext.Session.GetString("name");
            ViewBag.image = HttpContext.Session.GetString("image");
            var myContext = _context.ContactUsMessages.Include(c => c.User);
            return View(await myContext.ToListAsync());
        }



        // GET: ContactUs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ContactUsMessages == null)
            {
                return NotFound();
            }

            var contactUs = await _context.ContactUsMessages
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactUs == null)
            {
                return NotFound();
            }

            return View(contactUs);
        }

        // POST: ContactUs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ContactUsMessages == null)
            {
                return Problem("Entity set 'MyContext.ContactUsMessages'  is null.");
            }
            var contactUs = await _context.ContactUsMessages.FindAsync(id);
            if (contactUs != null)
            {
                _context.ContactUsMessages.Remove(contactUs);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactUsExists(int id)
        {
            return (_context.ContactUsMessages?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

