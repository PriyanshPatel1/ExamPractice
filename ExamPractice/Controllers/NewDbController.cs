using ExamPractice.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamPractice.Controllers
{
    public class NewDbController : Controller
    {
        private readonly ApplicationDataContext _context;

        public NewDbController(ApplicationDataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
           
            return View(await _context.Temps.ToListAsync());
        }



        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var emp = await _context.Temps.FirstOrDefaultAsync(m => m.Id == id);
            if (emp == null)
            {
                return NotFound();

            }
            return View(emp);
        }



        [HttpGet]
        public async Task<IActionResult> CreateT()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateT(temp temps)
        {
            if (ModelState.IsValid)
            {

               // temps.RegistrationDate = DateTime.Now;
                await _context.Temps.AddAsync(temps);
                await _context.SaveChangesAsync();


                return RedirectToAction(nameof(CreateT));

            }
            return View(temps);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var temp = await _context.Temps.FindAsync(id);
            if (temp == null)
            {
                return NotFound();
            }
            return View(temp);

        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, temp temps)
        {
            if (id != temps.Id)
            {
                return NotFound();

            }
            if (ModelState.IsValid)
            {
                try
                {
                   
                    _context.Update(temps);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TempsExits(temps.Id))
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

            return View(temps);
        }
        private bool TempsExits(int id)
        {
            return _context.Temps.Any(e => e.Id == id);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var temp = await _context.Temps
                .FirstOrDefaultAsync(m => m.Id == id);

            if (temp == null)
            {
                return NotFound();
            }

            return View(temp);

        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var temp = await _context.Temps.FindAsync(id);
            _context.Temps.Remove(temp);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));


        }

        public IEnumerable<temp> temps1 { get; set; }
        public void OnGet()
        {
            temps1 = _context.Temps.ToList();
        }

        public void OnPost()
        {
            temps1 = (from x in _context.Temps where (x.JoiningDate <= startdate) && (x.JoiningDate >= enddate) select x).ToList();
        }
    }
}
