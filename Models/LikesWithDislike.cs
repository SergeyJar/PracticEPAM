using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace PracticEPAM.Models;

public partial class LikesWithDislike
{
    public int Id { get; set; }

    public int Like { get; set; }

    public int Dislike { get; set; }

    public int IdProduct { get; set; }

    public string IdUser { get; set; } = null!;

    public virtual Product IdProductNavigation { get; set; } = null!;

    public virtual IdentityUser IdUserNavigation { get; set; } = null!;
}
