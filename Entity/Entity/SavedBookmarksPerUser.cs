using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class SavedBookmarksPerUser
    {
        [Key]
        public int ID { get; set; }

        public int BookmarkId{ get; set; }

        public virtual Bookmark Bookmark { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public int NumberClicked { get; set; }

        public SavedBookmarksPerUser(int bookmarkId, string userId)
        {
            BookmarkId = bookmarkId;
            UserId = userId;
        }
    }
}
