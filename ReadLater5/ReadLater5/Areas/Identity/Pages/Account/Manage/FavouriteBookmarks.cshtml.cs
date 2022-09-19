using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;

namespace ReadLater5.Areas.Identity.Pages.Account.Manage
{
    public partial class FavouriteBookmarks : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IBookmarkService _bookmarksService;

        public FavouriteBookmarks(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, IBookmarkService bookmarksService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _bookmarksService = bookmarksService;
        }

        public List<SavedBookmarksPerUser> Bookmarks { get; set; }


        private void Load()
        {

            Bookmarks = _bookmarksService.GetAllSavedBookmarksPerUser();

        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            Load();
            return Page();
        }
    }
}
