using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebLabsV06.DAL.Entities;

namespace WebLabsV06.DAL.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, UserRole, int>
    {
        public DbSet<Feed> Feeds { get; set; }
        public DbSet<FeedGroup> FeedGroups { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}
