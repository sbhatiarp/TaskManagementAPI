using Microsoft.EntityFrameworkCore;
using TaskManagement.Core.Entities;
using TaskManagement.Core.Interfaces;
using TaskManagement.Infrastructure.Data;

namespace TaskManagement.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskDbContext _context;

        public TaskRepository(TaskDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all tasks from the database.
        /// </summary>
        public async Task<IEnumerable<Tasks>> GetAllAsync()
        {
            return await _context.Tasks.ToListAsync();
        }

        /// <summary>
        /// Retrieves a task by its ID.
        /// Returns null if the task does not exist.
        /// </summary>
        public async Task<Tasks?> GetByIdAsync(int id)
        {
            return await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
        }

        /// <summary>
        /// Creates a new task in the database.
        /// </summary>as
        public async Task<Tasks> CreateAsync(Tasks task)
        {
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
            return task;
        }

        /// <summary>
        /// Updates an existing task.
        /// Throws KeyNotFoundException if the task does not exist.
        /// </summary>
        public async Task UpdateAsync(Tasks task)
        {
            if (!await ExistsAsync(task.Id))
                throw new KeyNotFoundException($"Task with ID {task.Id} not found.");

            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes a task if it exists.
        /// Throws KeyNotFoundException if the task does not exist.
        /// </summary>
        public async Task DeleteAsync(Tasks task)
        {
            if (!await ExistsAsync(task.Id))
                throw new KeyNotFoundException($"Task with ID {task.Id} not found.");

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Checks if a task with the given ID exists in the database.
        /// </summary>
        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Tasks.AnyAsync(e => e.Id == id);
        }
    }
}
