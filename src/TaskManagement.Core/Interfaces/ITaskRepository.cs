using TaskManagement.Core.Entities;

namespace TaskManagement.Core.Interfaces
{
    public interface ITaskRepository
    {
        Task<IEnumerable<Tasks>> GetAllAsync();
        Task<Tasks> GetByIdAsync(int id);
        Task<Tasks> CreateAsync(Tasks task);
        Task UpdateAsync(Tasks task);
        Task DeleteAsync(Tasks task);
        Task<bool> ExistsAsync(int id);
    }
} 