using BeonCollection.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace BeonCollection.Api.Data;

public class BeonDbContext : DbContext
{
    public BeonDbContext(DbContextOptions<BeonDbContext> options) : base(options)
    {
    }

    public DbSet<Game> Games => Set<Game>();
    public DbSet<Edition> Editions => Set<Edition>();
}