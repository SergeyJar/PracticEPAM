using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace PracticEPAM.Models;

public partial class Review
{
    public int IdReview { get; set; }

    public string IdUser { get; set; } = null!;

    public int IdProduct { get; set; }

    public string ReviewHtml { get; set; } = null!;

    public virtual ICollection<Comment>? Comments { get; set; } = new List<Comment>();

    public virtual Product? IdProductNavigation { get; set; } = null!;


}