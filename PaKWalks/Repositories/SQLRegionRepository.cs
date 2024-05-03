using Domain_OverView.Models.Domain_Model;
using Microsoft.EntityFrameworkCore;
using PaKWalks.DataBase;

namespace PaKWalks.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly PakWalkDbContext _dbcontext;

        public SQLRegionRepository(PakWalkDbContext pakWalkDb)
        {
            _dbcontext = pakWalkDb;
        }



        public async Task<Region> CreateAsync(Region region)
        {
            await _dbcontext.AddAsync(region);
            await _dbcontext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var existingRegion = await _dbcontext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegion == null)
            {
                return null;
            }
            _dbcontext.Regions.Remove(existingRegion);
            await _dbcontext.SaveChangesAsync();
            return existingRegion;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await _dbcontext.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await _dbcontext.Regions.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = await _dbcontext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegion == null)
            {
                return null;
            }
            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.RegionImageUrl = region.RegionImageUrl;
            await _dbcontext.SaveChangesAsync();
            return existingRegion;
        }
    }
}
