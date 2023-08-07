using BookList.Models;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BookListRazor.Pages.BookList
{
    public class UpsertModel : PageModel
    {
        private ApplicationDbContext _db;

        public UpsertModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Book Book { get; set; }

        public async Task<IActionResult> OnGet(int? id)
        {
            Book = new Book();
            if (id == null)
            {
                //Create
                return Page();
            }

            //Update
            Book = await _db.Books.FirstOrDefaultAsync(u => u.Id == id);
            if(Book == null)
            {
                return NotFound();
            }
            return Page();
            
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                if (ModelState.IsValid)
                {
                    if(Book.Id == 0)
                    {
                        _db.Books.Add(Book);
                    }
                    else
                    {
                        _db.Books.Update(Book);
                    }
                }

                await _db.SaveChangesAsync();

                return RedirectToPage("Index");
            }
            return RedirectToPage();
        }
    }
}
