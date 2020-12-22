using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EbookWebApi.Blazor.Data;
using EbookWebApi.Blazor.Models;
using Microsoft.AspNetCore.Components;

namespace EbookWebApi.Blazor.Pages
{
    public class BookListBase : ComponentBase
    {
        [Inject]
        public IBookRepository BookRepository { get; set; }

        public IEnumerable<Book> Books { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Books = (await BookRepository.GetAllBooks()).ToList();
        }
    }
}
