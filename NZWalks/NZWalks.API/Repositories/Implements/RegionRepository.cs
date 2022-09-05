using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories.Implements
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext _context;

        public RegionRepository(NZWalksDbContext context)
        {
            _context = context;
        }

        public async Task<Region> AddAsync(Region region)
        {
            region.Id = Guid.NewGuid();
            await _context.Regions.AddAsync(region);
            await _context.SaveChangesAsync();
            return region;
        }

        public async Task<Region> DeleteAsync(Guid id)
        {
            var region = await _context.Regions.FirstOrDefaultAsync(region => region.Id == id);
            if (region == null)
            {
                return null;
            }

            _context.Regions.Remove(region);
            await _context.SaveChangesAsync();
            return region;
        }

        public async Task<IEnumerable<Region>> GetAllAsync() => await _context.Regions.ToListAsync();

        public async Task<Region> GetAsync(Guid id) =>
            await _context.Regions.FirstOrDefaultAsync(region => region.Id == id);

        public async Task<Region> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = await _context.Regions.FirstOrDefaultAsync(region => region.Id == id);
            if (existingRegion == null)
            {
                return null;
            }

            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.Lat = region.Lat;
            existingRegion.Long = region.Long;
            existingRegion.Area = region.Area;
            existingRegion.Population = region.Population;

            await _context.SaveChangesAsync();
            return existingRegion;
        }
    }
}
