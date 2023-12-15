namespace API.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
            
        }

        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Country> Country { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                .HasOne(c => c.Country)
                .WithMany(a => a.Users)
                .HasForeignKey(s => s.CountryId)
                .OnDelete(DeleteBehavior.Restrict); 
        }
    }
}
