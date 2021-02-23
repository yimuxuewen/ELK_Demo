using System;
using System.Collections.Generic;
using System.Text;

namespace ELK
{
    public class Article
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public int Age { get; set; }
        public string[] Tags { get; set; }

        public override string ToString()
        {
            return $"Name:{Name},Title:{Title},Age:{Age},Tags:{Tags}";
        }
    }
}
