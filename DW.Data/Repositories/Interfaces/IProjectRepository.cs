using DW.Data.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DW.Data.Repositories.Interfaces
{
    public interface IProjectRepository
    {
        Task<DwProject> GetByIdAsync(string id);
        Task<IEnumerable<DwProject>> GetAllAsync();
        Task<DwProject> CreateAsync(DwProject entity);
        Task<DwProject> UpdateAsync(DwProject entity);
        Task<bool> DeleteAsync(string id);
    }
}
