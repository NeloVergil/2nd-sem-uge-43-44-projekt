using System;
using System.Collections.Generic;
using System.Text;

namespace PinkScoutClasslib.Interfaces
{
    public interface IActivityRepo
    {
        Task<Activity?> GetByIdAsync(int id); //spørgsmålstegn bare så den kan returnere null hvis aktiviteten ikke findes<33
        Task<IEnumerable<Activity>> GetAllAsync();
        Task AddAsync(Activity activity);
        Task UpdateAsync(Activity activity);
        Task DeleteAsync(int id);
        
    }
}
