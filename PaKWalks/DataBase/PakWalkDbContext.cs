using Domain_OverView.Models.Domain_Model;
using Microsoft.EntityFrameworkCore;

namespace PaKWalks.DataBase
{
    public class PakWalkDbContext : DbContext
    {
        public PakWalkDbContext(DbContextOptions<PakWalkDbContext> options) : base(options)
        {

        }
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }

        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data for Difficulties
            // Easy, Medium, Hard
            var difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id = Guid.Parse("54466f17-02af-48e7-8ed3-5a4a8bfacf6f"),
                    Name = "Easy"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c"),
                    Name = "Medium"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("f808ddcd-b5e5-4d80-b732-1ca523e48434"),
                    Name = "Hard"
                }
            };

            // Seed difficulties to the DataBase
            modelBuilder.Entity<Difficulty>().HasData(difficulties);


            // Seed data for Regions
            var regions = new List<Region>
            {
                new Region
                {
                    Id = Guid.Parse("f7248fc3-2585-4efb-8d1d-1c555f4087f6"),
                    Name = "Islamabad",
                    Code = "Isl",
                    RegionImageUrl = "This is image.jgp"
                },
                new Region
                {
                    Id = Guid.Parse("6884f7d7-ad1f-4101-8df3-7a6fa7387d81"),
                    Name = "Lahore",
                    Code = "Lah",
                    RegionImageUrl = "This is image.jgp"
                },
                new Region
                {
                    Id = Guid.Parse("14ceba71-4b51-4777-9b17-46602cf66153"),
                    Name = "Karachi",
                    Code = "Kch",
                    RegionImageUrl = "This is image.jgp"
                },
                new Region
                {
                    Id = Guid.Parse("cfa06ed2-bf65-4b65-93ed-c9d286ddb0de"),
                    Name = "Multan",
                    Code = "Mul",
                    RegionImageUrl = "This is image.jgp"
                },
                new Region
                {
                    Id = Guid.Parse("906cb139-415a-4bbb-a174-1a1faf9fb1f6"),
                    Name = "Vehari",
                    Code = "Veh",
                    RegionImageUrl = "This is image.jgp"
                },
                new Region
                {
                    Id = Guid.Parse("f077a22e-4248-4bf6-b564-c7cf4e250263"),
                    Name = "Sahiwal",
                    Code = "Sah",
                    RegionImageUrl = "This is image.jgp"
                },
            };

            // seed Region to  the Database
            modelBuilder.Entity<Region>().HasData(regions);
        }
    }
}
