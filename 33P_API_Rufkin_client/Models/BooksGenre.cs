using System;
using System.Collections.Generic;

namespace _33P_API_Rufkin_client.Models;

public partial class BooksGenre
{
    public int Id { get; set; }

    public int Book { get; set; }

    public int Genre { get; set; }

    public virtual Book BookNavigation { get; set; } = null!;

    public virtual Genre GenreNavigation { get; set; } = null!;
}
