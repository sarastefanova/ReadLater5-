using Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadLater5.Controllers
{
    [Authorize]
    public class BookmarksController : Controller
    {
        private readonly IBookmarkService bookmarkService;

        public BookmarksController(IBookmarkService bookmarkService)
        {
            this.bookmarkService = bookmarkService;
        }

        public IActionResult Index()
        {
            return View();
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category,Bookmark bookmark)
        {
            if (ModelState.IsValid)
            {
                bookmark.CategoryId = category.ID;
                bookmarkService.CreateBookmark(bookmark);
                return RedirectToAction("Index", "Categories");
            }

            return View(bookmark);
        }

        [Authorize(Roles = "Admin")]
        // GET: Bookmarks/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest);
            }
            Bookmark bookmark = bookmarkService.GetBookmark((int)id);
            if (bookmark == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound);
            }
            return View(bookmark);
        }

        [Authorize(Roles = "Admin")]
        // POST: Bookmarks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Bookmark bookmark)
        {
            if (ModelState.IsValid)
            {
                bookmarkService.UpdateBookmark(bookmark);
                return RedirectToAction("Index", "Categories");
            }
            return View(bookmark);
        }

        [Authorize(Roles = "Admin")]
        // GET: Bookmark/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest);
            }
            Bookmark bookmark = bookmarkService.GetBookmark((int)id);
            if (bookmark == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound);
            }
            return View(bookmark);
        }

        [Authorize(Roles = "Admin")]
        // POST: Bookmarks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Bookmark bookmark = bookmarkService.GetBookmark(id);
            bookmarkService.DeleteBookmark(bookmark);
            return RedirectToAction("Index", "Categories");
        }

        public IActionResult AddToFavourites(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest);
            }
            Bookmark bookmark = bookmarkService.GetBookmark((int)id);
            if (bookmark == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound);
            }
            bookmarkService.AddFavourite(bookmark);
            return RedirectToAction("Index", "Categories");
        }

        public IActionResult UpdateBookmarkCounterPerUser(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest);
            }
            SavedBookmarksPerUser savedBookmarksPerUser = bookmarkService.AddTUpdateBookmarkCounterPerUseroFavourites((int)id);
            if (savedBookmarksPerUser == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound);
            }
            //bookmarkService.AddFavourite(bookmark);
            return View(savedBookmarksPerUser);
        }

        public IActionResult DetailsPerUserBookmarks()
        {
            List<SavedBookmarksPerUser> savedBookmarks = bookmarkService.GetAllSavedBookmarksPerUser();
            return View("DetailsBookmarksSaved", savedBookmarks);
        }

    }
}
