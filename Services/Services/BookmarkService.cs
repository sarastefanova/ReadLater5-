using Data;
using Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class BookmarkService : IBookmarkService
    {
        private ReadLaterDataContext _ReadLaterDataContext;
        private readonly UserManager<ApplicationUser> _userManager;
        //private readonly HttpContext httpContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BookmarkService(ReadLaterDataContext readLaterDataContext, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            _ReadLaterDataContext = readLaterDataContext;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public Bookmark CreateBookmark(Bookmark bookmark)
        {
            bookmark.ID = 0;
            _ReadLaterDataContext.Bookmark.Add(bookmark);
            _ReadLaterDataContext.SaveChanges();
            return bookmark;
        }

        public void CreateBookmarksForCategory(List<Bookmark> bookmarks)
        {
            foreach (var bookmark in bookmarks)
            {
                CreateBookmark(bookmark);
            }
            _ReadLaterDataContext.SaveChanges();
        }

        public void DeleteBookmark(Bookmark bookmark)
        {
            _ReadLaterDataContext.Bookmark.Remove(bookmark);
            FindByBookmarkId(bookmark.ID);
            _ReadLaterDataContext.SaveChanges();
        }

        public void DeleteBookmarksForCategory(List<Bookmark> bookmarks)
        {
            foreach(var b in bookmarks)
            {
                DeleteBookmark(b);
            }
        }

        public Bookmark GetBookmark(int Id)
        {
            return _ReadLaterDataContext.Bookmark.Where(b => b.ID == Id).FirstOrDefault();
        }

        public List<Bookmark> GetBookmarks()
        {
            return _ReadLaterDataContext.Bookmark.ToList();
        }

        public List<Bookmark> GetBookmarksByCategoryId(int cateogryId)
        {
            return _ReadLaterDataContext.Bookmark.Where(b => b.CategoryId == cateogryId).ToList();
        }

        public void UpdateBookmark(Bookmark bookmark)
        {
            _ReadLaterDataContext.Update(bookmark);
            _ReadLaterDataContext.SaveChanges();
        }

        public void UpdateBookmarkByIdandCategory(int cateogryId, int id)
        {
            Bookmark updatedBookmark = _ReadLaterDataContext.Bookmark.Where(b => b.ID == id && b.CategoryId == cateogryId).FirstOrDefault();
            _ReadLaterDataContext.Update(updatedBookmark);
        }

        public void AddFavourite(Bookmark bookmark)
        {
           // var username = _httpContextAccessor.HttpContext.User.Identity.Name;

            var userid = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
            var user = _ReadLaterDataContext.Users.Where(x => x.Id == userid).FirstOrDefault();

            var savedBookmarkPerUser = new SavedBookmarksPerUser(bookmark.ID, userid)
            {
                ApplicationUser = user,
                Bookmark = bookmark
            };
            _ReadLaterDataContext.SavedBookmarksPerUser.Add(savedBookmarkPerUser);
            _ReadLaterDataContext.SaveChanges();
        }

        public List<SavedBookmarksPerUser> GetAllSavedBookmarksPerUser()
        {
            var userid = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
            return _ReadLaterDataContext.SavedBookmarksPerUser.Where(s => s.UserId.Equals(userid)).Include(c => c.Bookmark).ToList();


        }

        public List<SavedBookmarksPerUser> FindByBookmarkId(int Id)
        {
            return _ReadLaterDataContext.SavedBookmarksPerUser.Where(s => s.BookmarkId == Id).ToList();
        }

        public SavedBookmarksPerUser AddTUpdateBookmarkCounterPerUseroFavourites(int id)
        {
            SavedBookmarksPerUser savedBookmarksPerUser = _ReadLaterDataContext.SavedBookmarksPerUser.Where(s => s.ID == id).FirstOrDefault();
            savedBookmarksPerUser.NumberClicked = savedBookmarksPerUser.NumberClicked + 1;
            _ReadLaterDataContext.SaveChanges();
            return savedBookmarksPerUser;
        }
    }
}
