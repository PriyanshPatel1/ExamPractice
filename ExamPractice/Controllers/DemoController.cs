using ExamPractice.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamPractice.Controllers
{
    public class DemoController : Controller
    {

        private readonly ApplicationDataContext _context;

        public DemoController(ApplicationDataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string EmpSearch)
        {
            ViewData["getemployeedetails"] = EmpSearch;
            var empquery = from x in _context.employee select x;
            if (!string.IsNullOrEmpty(EmpSearch))
            {
                empquery = empquery.Where(x => x.Name.Contains(EmpSearch) || x.Email.Contains(EmpSearch));
            }
            return View(await empquery.AsNoTracking().ToListAsync());
            return View(await _context.employee.ToListAsync());
        }

        public async Task<IActionResult> Detail(int?id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var emp = await _context.employee.FirstOrDefaultAsync(m => m.Id == id);
            if (emp==null)
            {
                return NotFound();

            }
            return View(emp);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee emp)
        {
            if (ModelState.IsValid)
            {

                emp.RegistrationDate = DateTime.Now;
              await _context.employee.AddAsync(emp);
                await _context.SaveChangesAsync();

              
                return RedirectToAction(nameof(Index));

            }
            return View(emp);
        }
        public async Task<IActionResult> Login()
        {
            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Employee emp)
        {
            Employee LogginUser = _context.employee.Where(x => x.Email == emp.Email && x.Password == emp.Password).FirstOrDefault();
            if (LogginUser == null)
            {
                ViewBag.Message = "wrong username or password";
                return View();
            }
            HttpContext.Session.SetString("Email", LogginUser.Email);
            HttpContext.Session.SetString("Username", LogginUser.Name);
     
            return RedirectToAction("Welcomepage");
        }

        public async Task<IActionResult> Welcomepage()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Email")))
            {
                return RedirectToAction("Login");
            }

            ViewBag.Email = HttpContext.Session.GetString("Email");
          


            Response.Cookies.Append("LastLoggInTime", DateTime.Now.ToString());
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int?id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Employee = await _context.employee.FindAsync(id);
            if (Employee == null)
            {
                return NotFound();
            }
            return View(Employee);
            
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, Employee emp)
        {
            if (id != emp.Id)
            {
                return NotFound();

            }
            if (ModelState.IsValid)
            {
                try
                {
                    emp.RegistrationDate = DateTime.Now;
                    _context.Update(emp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExits(emp.Id))
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

            return View(emp);
        }
        private bool EmployeeExits(int id)
        {
            return _context.employee.Any(e => e.Id == id);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int?id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emp = await _context.employee
                .FirstOrDefaultAsync(m => m.Id == id);

            if (emp == null)
            {
                return NotFound();
            }

            return View(emp);

        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var emp = await _context.employee.FindAsync(id);
            _context.employee.Remove(emp);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
           

        }

        //[HttpGet]
        //public async Task<IActionResult> Search(string EmpSearch)
        //{
        //    ViewData["getemployeedetails"] = EmpSearch;
        //    var empquery = from x in _context.employee select x;
        //    if (!string.IsNullOrEmpty(EmpSearch))
        //    {
        //        empquery = empquery.Where(x => x.Name.Contains(EmpSearch) || x.Email.Contains(EmpSearch));
        //    }
        //    return View(await empquery.AsNoTracking().ToListAsync());
        //}
    }
}
