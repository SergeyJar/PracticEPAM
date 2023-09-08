using System;
using System.Collections.Generic;

namespace PracticEPAM.Models;

public partial class Product
{
    public int IdProduct { get; set; }

    public string Name { get; set; } = null!;

    public byte[] Photo { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int IdCategories { get; set; }

    public int LikesCount { get; set; }

    public int DislikesCount { get; set; }

    public virtual Category IdCategoriesNavigation { get; set; } = null!;

    public virtual ICollection<LikesWithDislike> LikesWithDislikes { get; set; } = new List<LikesWithDislike>();
}
