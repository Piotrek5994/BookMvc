using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int PageNumber { get; set; }
        public DateTime ReleaseDate { get; set; }
        public Category Category { get; set; }
        public int? AuthorId { get; set; } // Foreign key
        public Author? Author { get; set; }
        public int? PublishingHouseId { get; set; } // Foreign key
        public PublishingHouse? PublishingHouse { get; set; }
    }
}
