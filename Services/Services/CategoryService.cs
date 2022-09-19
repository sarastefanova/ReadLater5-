using Data;
using Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Services
{
    public class CategoryService : ICategoryService
    {
        private ReadLaterDataContext _ReadLaterDataContext;
        private readonly IBookmarkService bookmarkService;
        public CategoryService(ReadLaterDataContext readLaterDataContext, IBookmarkService bookmarkService) 
        {
            _ReadLaterDataContext = readLaterDataContext;
            this.bookmarkService = bookmarkService;
        }

        public Category CreateCategory(Category category)
        {
            _ReadLaterDataContext.Add(category);
            //_ReadLaterDataContext.SaveChanges();
            bookmarkService.CreateBookmarksForCategory(category.Bookmarks);
            _ReadLaterDataContext.SaveChanges();
            return category;
        }

        public void UpdateCategory(Category category)
        {
            _ReadLaterDataContext.Update(category);
            _ReadLaterDataContext.SaveChanges();
        }

        public List<Category> GetCategories()
        {
            return _ReadLaterDataContext.Categories.ToList();
        }

        public Category GetCategory(int Id)
        {
            Category category = _ReadLaterDataContext.Categories.Where(c => c.ID == Id).FirstOrDefault();
            List<Bookmark> bookmarks = bookmarkService.GetBookmarksByCategoryId(Id);
            category.Bookmarks = bookmarks;
            return category;
        }

        public Category GetCategory(string Name)
        {
            return _ReadLaterDataContext.Categories.Where(c => c.Name == Name).FirstOrDefault();
        }

        public void DeleteCategory(Category category)
        {
            _ReadLaterDataContext.Categories.Remove(category);
            bookmarkService.DeleteBookmarksForCategory(category.Bookmarks);
            _ReadLaterDataContext.SaveChanges();
        }
    }
}
