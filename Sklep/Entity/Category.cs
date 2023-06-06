using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklep.Entity
{
    public class category
    {
        public int? ID { get; set; }
        public string? Title { get; set; }

        public category(int? iD, string? title)
        {
            ID = iD;
            Title = title;
        }

        public category(string? title)
        {
            Title= title;
        }
    }
}
