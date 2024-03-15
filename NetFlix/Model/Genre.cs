using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Model
{
    public class Genre
    {
        private int __id; 
        private string __name;

        public int Id { get => __id; set => __id = value; }
        public string Name { get => __name; set => __name = value; }
    }
}
