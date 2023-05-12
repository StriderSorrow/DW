using DW.Data.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DW.Data.Repositories.Interfaces
{
    public interface ITrackRepository
    {
        Task<DwMedia> GetByIdAsync(string id);
        Task<IEnumerable<DwMedia>> GetAllAsync();
        Task<DwMedia> CreateAsync(DwMedia entity);
        Task<DwMedia> UpdateAsync(DwMedia entity);
        Task<bool> DeleteAsync(string id);
    }
}
