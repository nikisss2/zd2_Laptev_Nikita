using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zd3_Laptev_Nikita
{
    struct Song
    {
        public string Author;
        public string Title;
        public string Filename;

        public Song(string author, string title, string filename)
        {
            Author = author;
            Title = title;
            Filename = filename;
        }

        // вывод в списке
        public override string ToString()
        {
            return $"{Author} - {Title} ({Filename})";
        }
    }
}