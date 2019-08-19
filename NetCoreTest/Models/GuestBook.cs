using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using static NetCoreTest.Models.CoreTextDatabaseContext;

namespace NetCoreTest.Models
{
    public class GuestBook
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int GuestBookId { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
