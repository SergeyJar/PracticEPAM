using System;
using System.Collections.Generic;

namespace PracticEPAM.Models;

public partial class Category
{
    public int IdCategories { get; set; }

    public string Category1 { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
