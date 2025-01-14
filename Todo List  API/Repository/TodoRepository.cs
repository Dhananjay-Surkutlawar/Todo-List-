using AutoMapper;
using Todo_List__API.Contracts;
using Todo_List__API.DatabaseTables;
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

        public async Task AddTodoAsync(TodoContract todoContact)
        {
            // Map TodoContact to TodoEntity
            var todoEntity = _mapper.Map<TodoEntity>(todoContact);

            // Add the TodoEntity to the DbContext
            await _dbContext.Todos.AddAsync(todoEntity);

            // Save the changes to the database
            await _dbContext.SaveChangesAsync();
        }
    }
}
