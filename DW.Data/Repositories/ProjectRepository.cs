using DW.Data.Database;
using DW.Data.Database.Entities;
using DW.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DW.Data.Repositories
{
    internal class ProjectRepository : IProjectRepository
    {
        private readonly DwDbContext _context;

        public ProjectRepository(DwDbContext context)
        {
            _context = context;
        }

        public async Task<DwProject> CreateAsync(DwProject entity)
        {
            entity.Id = Guid.NewGuid().ToString();
            _context.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Projects.FindAsync(id) ?? throw new KeyNotFoundException();
            entity.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<DwProject>> GetAllAsync()
        {
            return _context.Projects.Where(x => !x.IsDeleted).ToList();
        }

        public async Task<DwProject> GetByIdAsync(string id)
        {
            return await _context.Projects.FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == id) ?? throw new KeyNotFoundException();
        }

        public async Task<DwProject> UpdateAsync(DwProject entity)
        {
            _context.Projects.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<DwProject> AddUserAsync(DwUser participant, string id)
        {
            var entity = await _context.Projects.Include(x => x.Participants).FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == id) ?? throw new KeyNotFoundException();

            entity.Participants.Add(participant);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
