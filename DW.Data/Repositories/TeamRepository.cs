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
    internal class TeamRepository:ITeamRepository
    {
        private readonly DwDbContext _context;

        public TeamRepository(DwDbContext context)
        {
            _context = context;
        }

        public async Task<DwTeam> CreateAsync(DwTeam entity)
        {
            entity.Id = Guid.NewGuid().ToString();
            _context.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Teams.FindAsync(id)??throw new KeyNotFoundException();
            entity.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<DwTeam>> GetAllAsync()
        {
            return _context.Teams.Where(x=>!x.IsDeleted).ToList();
        }

        public async Task<DwTeam> GetByIdAsync(string id)
        {
            return await _context.Teams.FirstOrDefaultAsync(x=>!x.IsDeleted && x.Id == id) ?? throw new KeyNotFoundException();
        }

        public async Task<DwTeam> UpdateAsync(DwTeam entity)
        {
            _context.Teams.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<DwTeam>AddUserAsync(DwUser user, string id)
        {
            var entity = await _context.Teams.Include(x=>x.Users).FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == id) ?? throw new KeyNotFoundException();
            
            entity.Users.Add(user);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
