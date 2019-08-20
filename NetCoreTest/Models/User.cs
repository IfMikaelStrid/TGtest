using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreTest.Models
{
    public class User
    {
        [Key]
        public string UserId { get; set; }
        public string UserImageURL { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
    }
}
