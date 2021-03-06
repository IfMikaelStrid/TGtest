﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using static NetCoreTest.Models.CoreTextDatabaseContext;

namespace NetCoreTest.Models
{
    public class Post
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public string PostId { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishTimeStamp { get; set; }
        public string AuthorId { get; set; }
        public List<Upvote> Upvotes { get; set; }
        public int GuestBookId { get; set; }
    }
}
