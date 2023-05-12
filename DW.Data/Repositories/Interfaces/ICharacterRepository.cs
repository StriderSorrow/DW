using DW.Data.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DW.Data.Repositories.Interfaces
{
    public interface ICharacterRepository
    {
        Task<DwCharacter> GetByIdAsync(string id);
        Task<IEnumerable<DwCharacter>> GetAllAsync();
        Task<DwCharacter> CreateAsync(DwCharacter entity);
        Task<DwCharacter> UpdateAsync(DwCharacter entity);
        Task<bool> DeleteAsync(string id);
    }
}
