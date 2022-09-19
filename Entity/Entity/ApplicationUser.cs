using Entity;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Entity
{
    public class ApplicationUser: IdentityUser
    {
        public List<Bookmark> SavedBookmarks { get; set; }
    }
}
