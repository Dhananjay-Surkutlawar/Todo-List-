using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Todo_List__API.Contracts;
using Todo_List__API.Repository;

namespace Todo_List__API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoRepository _todoRepository;

        public TodoController(TodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        // POST: api/Todo
        [HttpPost]
        public async Task<IActionResult> AddTodoAsync([FromBody] TodoContract todoContract)
        {
            if (todoContract == null)
            {
                return BadRequest("TodoContract is null.");
            }

            // Call the repository method to add the Todo
            await _todoRepository.AddTodoAsync(todoContract);

            return Ok("Todo added successfully.");
        }
    }
}
