using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EbookWebApi.Blazor.Models
{
    public class Category
    {
        public int ID { get; set; }
        public string Nama { get; set; }
        public List<Book> Books { get; set; }
    }
}
