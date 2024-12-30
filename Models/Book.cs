using System.ComponentModel.DataAnnotations;

namespace BookstoreApi.Models
{
    public class Book
    {
        public int Id { get; set; }

        public required string Title { get; set; }

        public required string Author { get; set; }

        public required string Gender { get; set; }

        public double Price { get; set; }

        public int Stock { get; set; }
    }
}
