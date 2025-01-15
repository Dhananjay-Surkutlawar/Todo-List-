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

        [HttpGet]
        public async Task<IActionResult> GetAllTodoList()
        {
           
            List<TodoContract> contracts = await _todoRepository.GetAllTodoList();

            return Ok(contracts);
        }


        [HttpPut("update")]
        public async Task<IActionResult> UpdateTodoAsync([FromBody] TodoContract todoContract)
        {
            if (todoContract == null)
            {
                return BadRequest("TodoContract is null.");
            }

            // Call the repository method to Update the Todo
            await _todoRepository.UpdateTodoAsync(todoContract);

            return Ok("Todo added successfully.");
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteTodoAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid ID.");
            }

            try
            {
                // Call the repository method to delete the Todo by ID
                await _todoRepository.DeleteToDo(id);

                // Return a success message if deletion is successful
                return Ok("Todo deleted successfully.");
            }
            catch (KeyNotFoundException)
            {
                // If the Todo with the given ID is not found, return NotFound
                return NotFound("Todo not found.");
            }
            catch (Exception ex)
            {
                // General error handling for any unexpected errors
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }




    }
}
