using AutoMapper;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Todo_List__API.Contracts;
using Todo_List__API.DatabaseTables;
using Todo_List__API.Email_Service;
using Todo_List__API.Entity;

namespace Todo_List__API.Repository
{
    public class TodoRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public TodoRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        //public async Task AddTodoAsync(TodoContract todoContact)
        //{
        //    // Map TodoContact to TodoEntity
        //    var todoEntity = _mapper.Map<TodoEntity>(todoContact);

        //    // Add the TodoEntity to the DbContext
        //    await _dbContext.Todos.AddAsync(todoEntity);

        //    // Save the changes to the database
        //    await _dbContext.SaveChangesAsync();
        //}


        public async Task AddTodoAsync(TodoContract todoContact)
        {
            // Map TodoContact to TodoEntity
            var todoEntity = _mapper.Map<TodoEntity>(todoContact);

            // Add the TodoEntity to the DbContext
            await _dbContext.Todos.AddAsync(todoEntity);

            // Save the changes to the database
            await _dbContext.SaveChangesAsync();

            // Get the email from the TodoEntity
            var userEmail = todoEntity.Email;

            // Enqueue the email job to send the notification
            if (!string.IsNullOrEmpty(userEmail))
            {
                BackgroundJob.Enqueue<TodoNotificationJob>(job => job.SendTodoNotificationAsync(userEmail, todoEntity.Title));
            }
        }

        public async Task<List<TodoContract>> GetAllTodoList()
        {
            // Fetches a list of TodoEntity records from the database where IsCompleted is false.
            List<TodoEntity> list = await _dbContext.Todos
                .Where(c => c.IsCompleted == false) // Filters only incomplete tasks.
                .ToListAsync(); // Asynchronously retrieves the filtered list.

            // Maps the list of TodoEntity objects to a list of TodoContract objects.
            List<TodoContract> contracts = _mapper.Map<List<TodoContract>>(list);

            // Returns the mapped list of TodoContract objects to the caller.
            return contracts;
        }

        public async Task UpdateTodoAsync(TodoContract todoContact)
        {
            // Map TodoContract to TodoEntity
            var todoEntity = _mapper.Map<TodoEntity>(todoContact);

            // Update the TodoEntity in the DbContext
            _dbContext.Todos.Update(todoEntity);

            // Save the changes to the database
            await _dbContext.SaveChangesAsync();
        }


        public async Task DeleteToDo(Guid id)
        {
            // Find the Todo entity by its Id (Guid)
            var todoEntity = await _dbContext.Todos.FindAsync(id);

            // Check if the entity exists
            if (todoEntity == null)
            {
                // If not found, throw an exception or handle it accordingly
                throw new KeyNotFoundException("Todo not found.");
            }

            // Remove the Todo entity from the DbContext
            _dbContext.Todos.Remove(todoEntity);

            // Save the changes to the database (i.e., delete the record)
            await _dbContext.SaveChangesAsync();
        }


    }
}
