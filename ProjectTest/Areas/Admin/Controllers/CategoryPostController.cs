﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectTest.Models;

namespace ProjectTest.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryPostController : BaseController
    {
        private readonly MyBlogDbContext _context;

        public CategoryPostController(MyBlogDbContext context)
        {
            _context = context;
        }

        // GET: Admin/CategoryPost
        public async Task<IActionResult> Index()
        {
            return View(await _context.CategoryPosts.ToListAsync());
        }

        // GET: Admin/CategoryPost/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryPost = await _context.CategoryPosts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoryPost == null)
            {
                return NotFound();
            }

            return View(categoryPost);
        }

        // GET: Admin/CategoryPost/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/CategoryPost/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CategoryName,MetaTitle,MetaKeyword,MetaDescription,CreatedDate,ModifiedDate,Status")] CategoryPost categoryPost)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoryPost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoryPost);
        }

        // GET: Admin/CategoryPost/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryPost = await _context.CategoryPosts.FindAsync(id);
            if (categoryPost == null)
            {
                return NotFound();
            }
            return View(categoryPost);
        }

        // POST: Admin/CategoryPost/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,CategoryName,MetaTitle,MetaKeyword,MetaDescription,CreatedDate,ModifiedDate,Status")] CategoryPost categoryPost)
        {
            if (id != categoryPost.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoryPost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryPostExists(categoryPost.Id))
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
            return View(categoryPost);
        }

        // GET: Admin/CategoryPost/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryPost = await _context.CategoryPosts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoryPost == null)
            {
                return NotFound();
            }

            return View(categoryPost);
        }

        // POST: Admin/CategoryPost/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var categoryPost = await _context.CategoryPosts.FindAsync(id);
            _context.CategoryPosts.Remove(categoryPost);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryPostExists(long id)
        {
            return _context.CategoryPosts.Any(e => e.Id == id);
        }
    }
}
