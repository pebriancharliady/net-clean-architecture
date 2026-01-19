using System;
using Microsoft.EntityFrameworkCore;
using PebriBox.Domain.Entities;

namespace PebriBox.Infrastructure.Contexts;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    public DbSet<Agent> Agents { get; set; }
    public DbSet<Property> Properties { get; set; }
}
