using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LeThiHoa251.Data;
using LeThiHoa251.Models;

namespace CompanyLTH251.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Student
        public async Task<IActionResult> Index()
        {
              return _context.Companys != null ? 
                          View(await _context.Companys.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Students'  is null.");
        }

        // GET: Student/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Companys == null)
            {
                return NotFound();
            }
            //tìm dữ liệu trong database theo id
            var company = await _context.Companys
                .FirstOrDefaultAsync(m => m.Id == id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // GET: Company/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Company/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullName")] Company compaty)
        {
            if (ModelState.IsValid)
            {
                //add vào data
                _context.Add(compaty);
                //lưu thay đổi vào db
                await _context.SaveChangesAsync();
                //sau khi lưu thau đổi, điều hương về trang index
                return RedirectToAction(nameof(Index));
            }
            return View(compaty);
        }
        //Tạo phương thức Edit kiểm tra xem “id” của sinh viên có tồn tại trong cơ sở dữ liệu không? Nếu có thì trả về view “Edit” cho phép người dùng chỉnh sửa thông tin của Sinh viên đó.​
        // GET: Company/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Companys == null)
            {
                return NotFound();
            }

            var compaty = await _context.Companys.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(compaty);
        }
        //Tạo phương thức Edit cập nhật thông tin của sinh viên theo mã sinh viên.​
        // POST: Company/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName")] Company compaty)
        {
            if (id != compaty.Id)
            {
                //return NotFound()
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //update dữ liệu thay đổi vào applicationDb
                    _context.Update(compaty);
                    //lư thay đổi
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(compaty.Id))
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
            return View(compaty);
        }
        //Tạo phương thức Delete kiểm tra xem “id” của sinh viên có tồn tại trong cơ sở dữ liệu không? Nếu có thì trả về view “Delete” cho phép người dùng xoá thông tin của Sinh viên đó.​
        // GET: Student/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Company == null)
            {
                return NotFound();
            }

            var student = await _context.Company
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(compaty);
        }
        // Tạo phương thức Delete xoá thông tin của sinh viên theo mã sinh viên.​
        // POST: Student/Delete/5 
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Company == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Students'  is null.");
            }
            var compaty = await _context.Company.FindAsync(id);
            if (compaty != null)
            {
                _context.Students.Remove(compaty);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // Tạo phương thức kiểm tra 1 sinh viên theo mã sinh viên có tồn tại trong cơ sở dữ liệu không?​
        private bool StudentExists(int id)
        {
          return (_context.Company?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}