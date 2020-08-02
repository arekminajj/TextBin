using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TextBin.Data;
using TextBin.Models;

namespace TextBin.Controllers
{
    public class TextsController : Controller
    {
        private readonly TextBinContext _context;

        public TextsController(TextBinContext context)
        {
            _context = context;
        }

        // GET: Texts
        public IActionResult Index()
        {
            return RedirectToAction("Create", "Texts");
        }

        // GET: Texts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Texts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Content,Title")] Text text)
        {
            if (ModelState.IsValid)
            {
                string stringId = RandomString(6);

                text.Created = DateTime.Now;
                text.StringId = stringId;
                _context.Add(text);
                await _context.SaveChangesAsync();
                return RedirectToAction("ThankYou", new { idString = stringId });
            }
            return View(text);
        }

        public IActionResult ThankYou(string idString)
        {
            ViewData["Link"] = "https://localhost:44397/Texts/Get?idString=" + idString;
            return View();
        }

        public IActionResult Get(string idString)
        {
            if (TextExists(idString))
            {
                var text = _context.Text.FirstOrDefault(e => e.StringId == idString);
                return View(text);
            }

            return Content("Could not find text");
        }

        private bool TextExists(string idString)
        {
            return _context.Text.Any(e => e.StringId == idString);
        }

        private bool TextExists(int id)
        {
            return _context.Text.Any(e => e.Id == id);
        }

        private readonly Random _random = new Random();


        // Generates a random string with a given size.    
        public string RandomString(int size, bool lowerCase = false)
        {
            var builder = new StringBuilder(size);

            // Unicode/ASCII Letters are divided into two blocks
            // (Letters 65–90 / 97–122):   
            // The first group containing the uppercase letters and
            // the second group containing the lowercase.  

            // char is a single Unicode character  
            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26; // A...Z or a..z: length = 26  

            for (var i = 0; i < size; i++)
            {
                var @char = (char)_random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }
    }
}
