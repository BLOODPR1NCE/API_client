using System;
using System.Collections.Generic;

namespace _33P_API_Rufkin_client.Models;

public partial class Genre
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<BooksGenre> BooksGenres { get; set; } = new List<BooksGenre>();
}
