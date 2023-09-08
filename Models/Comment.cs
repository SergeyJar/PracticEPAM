using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace PracticEPAM.Models;

public partial class Comment
{
    public int IdComment { get; set; }

    public int IdReview { get; set; }

    public string Text { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public DateTime? EditDate { get; set; }

    public string IdUser { get; set; } = null!;

    public int? IdBase { get; set; }

    public virtual Review IdReviewNavigation { get; set; } = null!;

    public virtual IdentityUser IdUserNavigation { get; set; } = null!;
}
