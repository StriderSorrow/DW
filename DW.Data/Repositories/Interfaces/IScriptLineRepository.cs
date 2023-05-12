using DW.Data.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DW.Data.Repositories.Interfaces
{
    public interface IScriptLineRepository
    {
        Task<DwScriptLine> GetByIdAsync(string id);
        Task<IEnumerable<DwScriptLine>> GetAllAsync();
        Task<DwScriptLine> CreateAsync(DwScriptLine entity);
        Task<DwScriptLine> UpdateAsync(DwScriptLine entity);
        Task<bool> DeleteAsync(string id);
    }
}
