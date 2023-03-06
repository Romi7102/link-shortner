﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace LinkShortner.Models {
    public class Link {
        [Key]
        public string Code { get; set; }
        [Required]
        public string FullUrl { get; set; }
        public int Clicks { get; set; } = 0;

        public string UserId { get; set; }
        public DateTime? Created { get; set; } = DateTime.UtcNow;
        public virtual List<LinkLog> Logs { get; set; }

    }
}
