using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebLabsV06.DAL.Data;
using WebLabsV06.DAL.Entities;

namespace Dashkevich_90331.Areas.Admin.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly WebLabsV06.DAL.Data.ApplicationDbContext _context;

        public DeleteModel(WebLabsV06.DAL.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Feed Feed { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Feed = await _context.Feeds
                .Include(f => f.Group).FirstOrDefaultAsync(m => m.FeedId == id);

            if (Feed == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Feed = await _context.Feeds.FindAsync(id);

            if (Feed != null)
            {
                _context.Feeds.Remove(Feed);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
