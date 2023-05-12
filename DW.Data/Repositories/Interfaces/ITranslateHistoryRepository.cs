using DW.Data.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DW.Data.Repositories.Interfaces
{
    public interface ITranslateHistoryRepository
    {
        Task<DwTranslateHistory> GetByIdAsync(string id);
        Task<IEnumerable<DwTranslateHistory>> GetAllAsync();
        Task<DwTranslateHistory> CreateAsync(DwTranslateHistory entity);
        Task<DwTranslateHistory> UpdateAsync(DwTranslateHistory entity);
    }
}
