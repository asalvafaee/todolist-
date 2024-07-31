using Microsoft.AspNetCore.Mvc;
using Todo;
using TodoList.Data;

namespace TodoList.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoItemsController(TodoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetTodoItems()
        {
            return Ok(_context.TodoItems.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetTodoItem(int id)
        {
            var todoItem = _context.TodoItems.Find(id);
            if (todoItem == null)
            {
                return NotFound();
            }
            return Ok(todoItem);
        }

        [HttpPost]
        public IActionResult CreateTodoItem(TodoItem todoItem)
        {
            _context.TodoItems.Add(todoItem);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, todoItem);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTodoItem(int id, TodoItem todoItem)
        {
            var existingItem = _context.TodoItems.Find(id);
            if (existingItem == null)
            {
                return NotFound();
            }
            existingItem.Title = todoItem.Title;
            existingItem.Description = todoItem.Description;
            existingItem.IsCompleted = todoItem.IsCompleted;
            _context.SaveChanges();
            return Ok(existingItem);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTodoItem(int id)
        {
            var todoItem = _context.TodoItems.Find(id);
            if (todoItem == null)
            {
                return NotFound();
            }
            _context.TodoItems.Remove(todoItem);
            _context.SaveChanges();
            return NoContent();
        }
    }
}