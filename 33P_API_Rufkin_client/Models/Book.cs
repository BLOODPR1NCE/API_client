using System;
using System.Collections.Generic;

namespace _33P_API_Rufkin_client.Models;

public partial class Book
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public int Author { get; set; }

    public int YearPublication { get; set; }

    public virtual Author AuthorNavigation { get; set; } = null!;

    public virtual ICollection<BooksGenre> BooksGenres { get; set; } = new List<BooksGenre>();
}
