using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Introduction.Models
{
    /// <summary>
    /// Model untuk proses CRUD pada API
    /// </summary>
    public class CategoryDto
    {
        /// <summary>
        /// Category ID
        /// </summary>
        [Required]
        public int Id { get; set; }
        /// <summary>
        /// Nama Kategori
        /// </summary>
        [Required]
        [MaxLength(50, ErrorMessage = "Nama tidak boleh lebih dari 50 karakter")]
        public string Nama { get; set; }
        /// <summary>
        /// Data list Book untuk insert multiple book
        /// </summary>
        public ICollection<BookDto> Books { get; set; }
            = new List<BookDto>();

    }
}
