using DW.Data.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DW.Data.Repositories.Interfaces
{
    public interface ITaskRepository
    {
        Task<DwTask> GetByIdAsync(string id);
        Task<IEnumerable<DwTask>> GetAllAsync();
        Task<DwTask> CreateAsync(DwTask entity);
        Task<DwTask> UpdateAsync(DwTask entity);
        Task<bool> DeleteAsync(string id);
    }
}
