using Entity;
using System.Collections.Generic;

namespace Services
{
    public interface IBookmarkService
    {
        Bookmark CreateBookmark(Bookmark bookmark);
        List<Bookmark> GetBookmarks();
        Bookmark GetBookmark(int Id);
        void UpdateBookmark(Bookmark bookmark);
        void DeleteBookmark(Bookmark bookmark);
        List<Bookmark> GetBookmarksByCategoryId(int cateogryId);
        void UpdateBookmarkByIdandCategory(int cateogryId, int id);

        void CreateBookmarksForCategory(List<Bookmark> bookmarks);
        void DeleteBookmarksForCategory(List<Bookmark> bookmarks);

        void AddFavourite(Bookmark bookmark);

        List<SavedBookmarksPerUser> GetAllSavedBookmarksPerUser();

        List<SavedBookmarksPerUser> FindByBookmarkId(int Id);
        SavedBookmarksPerUser AddTUpdateBookmarkCounterPerUseroFavourites(int id);
    }
}
