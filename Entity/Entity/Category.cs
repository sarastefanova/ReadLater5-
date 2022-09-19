﻿
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Entity
{
    public class Category
    {
        [Key]
        public int ID { get; set; }

        [StringLength(maximumLength: 50)]
        public string Name { get; set; }

        public List<Bookmark> Bookmarks{ get; set; }
        public Category()
        {
            Bookmarks = new List<Bookmark>();
        }
    }
}
