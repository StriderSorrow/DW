using DW.Data.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DW.Data.Repositories.Interfaces
{
    public interface ITeamRepository
    {
        Task<DwTeam> GetByIdAsync(string id);
        Task<IEnumerable<DwTeam>> GetAllAsync();
        Task<DwTeam> CreateAsync(DwTeam entity);
        Task<DwTeam> UpdateAsync(DwTeam entity);
        Task<bool> DeleteAsync(string id);
        Task<DwTeam> AddUserAsync(DwUser user, string id);
    }
}
