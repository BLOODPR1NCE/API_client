using System;
using System.Collections.Generic;

namespace _33P_API_Rufkin_client.Models;

public partial class Author
{
    public int Id { get; set; }

    public string Fullname { get; set; } = null!;

    public DateOnly Birthdate { get; set; }

    public string Country { get; set; } = null!;

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
