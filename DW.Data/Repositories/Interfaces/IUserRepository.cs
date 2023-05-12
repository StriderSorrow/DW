using DW.Data.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DW.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<DwUser> GetByIdAsync(string id);
        Task<IEnumerable<DwUser>> GetAllAsync();
        Task<DwUser> CreateAsync(DwUser entity);
        Task<DwUser> UpdateAsync(DwUser entity);
        Task<bool> DeleteAsync(string id);
    }
}
