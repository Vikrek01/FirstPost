using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstBlog1.Models
{
    public class User
    {
        public Guid UserId { get; set; } = Guid.NewGuid();
        public string UserName { get; set; }    
        public string Password { get; set; }
        public string Role { get; set; } = "User";

        public ICollection<Blog> Blogs { get; set; }
    }
}