using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstBlog1.Models
{
    public class Blog
    {
        public int Id { get; set; }

    
        public string Image { get; set; }

        public string Heading { get; set; }
        public string Description { get; set; }
            
        public string Author { get; set; }
        public Guid Store { get; set; }
        public virtual User Users { get; set; }
        
    }
}