using TaskManagement.Application.DTOs;
using TaskManagement.Application.Interfaces;
using TaskManagement.Core.Entities;
using TaskManagement.Core.Interfaces;

namespace TaskManagement.Application.Services
{
    /// <summary>
    /// Service class for handling task-related operations.
    /// </summary>
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        /// <summary>
        /// Initializes a new instance of <see cref="TaskService"/>.
        /// </summary>
        /// <param name="taskRepository">Repository interface for task operations.</param>
        public TaskService(ITaskRepository taskRepository) => _taskRepository = taskRepository;

        /// <summary>
        /// Retrieves all tasks asynchronously.
        /// </summary>
        /// <returns>A collection of TaskDto objects.</returns>
        public async Task<IEnumerable<TaskDto>> GetAllTasksAsync() =>
            (await _taskRepository.GetAllAsync()).Select(MapToDto);

        /// <summary>
        /// Retrieves a specific task by its ID.
        /// </summary>
        /// <param name="id">Task ID.</param>
        /// <returns>TaskDto object if found; otherwise, throws an exception.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when the task ID does not exist.</exception>
        public async Task<TaskDto?> GetTaskByIdAsync(int id) =>
            MapToDto(await _taskRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"Task with ID {id} not found."));

        /// <summary>
        /// Creates a new task asynchronously.
        /// </summary>
        /// <param name="taskDto">DTO containing task details.</param>
        /// <returns>The created TaskDto object.</returns>
        public async Task<TaskDto> CreateTaskAsync(CreateTaskDto taskDto)
        {
            var task = new Tasks
            {
                Description = taskDto.Description,
                CreatedDate = DateTime.UtcNow,
                IsCompleted = taskDto.IsCompleted
            };

            await _taskRepository.CreateAsync(task);
            return MapToDto(task);
        }

        /// <summary>
        /// Updates an existing task by its ID.
        /// </summary>
        /// <param name="id">Task ID.</param>
        /// <param name="taskDto">DTO containing updated task details.</param>
        /// <exception cref="KeyNotFoundException">Thrown when the task ID does not exist.</exception>
        public async Task UpdateTaskAsync(int id, UpdateTaskDto taskDto)
        {
            var task = await _taskRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"Task with ID {id} not found.");

            task.Description = taskDto.Description;
            task.IsCompleted = taskDto.IsCompleted;

            await _taskRepository.UpdateAsync(task);
        }

        /// <summary>
        /// Deletes a task by its ID.
        /// </summary>
        /// <param name="id">Task ID.</param>
        /// <exception cref="KeyNotFoundException">Thrown when the task ID does not exist.</exception>
        public async Task DeleteTaskAsync(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"Task with ID {id} not found.");

            await _taskRepository.DeleteAsync(task);
        }

        /// <summary>
        /// Maps a Task entity to a TaskDto.
        /// </summary>
        /// <param name="task">Task entity.</param>
        /// <returns>Mapped TaskDto object.</returns>
        private static TaskDto MapToDto(Tasks task) => new()
        {
            Id = task.Id,
            Description = task.Description,
            CreatedDate = task.CreatedDate,
            IsCompleted = task.IsCompleted
        };
    }
}
