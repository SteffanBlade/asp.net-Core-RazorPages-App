﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorApp.Model;

namespace RazorApp.Pages.BookList
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDBContext _db;

        public EditModel(ApplicationDBContext db)
        {
            _db = db;

        }

        [BindProperty]
        public Book Book { get; set; }
        public async Task OnGet(int id)
        {
            Book = await _db.Book.FindAsync(id);
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                int id = Book.Id;
                var BookFromDb = await _db.Book.FindAsync(id);
                BookFromDb.Name = Book.Name;
                BookFromDb.Author = Book.Author;
                BookFromDb.ISBN = Book.ISBN;

                await _db.SaveChangesAsync();

                return RedirectToPage("Index");
            }
            else
            {
                return RedirectToPage();
            }
            
        }

    }
}