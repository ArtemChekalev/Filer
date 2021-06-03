using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filer
{
    class Book
    {
        public string Name;
        public string Price;
        public string Rating;
        public string Author;
        public string Link;
        public Book()
        {

        }
        public Book(string name, string price, string rating, string author, string link)
        {
            Name = name;
            Price = price;
            Rating = rating;
            Author = author;
            Link = link;
        }
    }
}
