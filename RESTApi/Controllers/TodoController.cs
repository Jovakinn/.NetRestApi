using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RESTApi.Models;

namespace RESTApi.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoItemsController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
        {
            Debug.Assert(_context.TodoItems != null, "_context.TodoItems != null");
            return await _context.TodoItems
                .Select(x => ItemToDto(x))
                .ToListAsync();
        }

        // GET: api/TodoItems/5
        [HttpGet("{id:long}")]
        public async Task<ActionResult<TodoItemDTO>?> GetTodoItem(long id)
        {
            if (_context.TodoItems == null) return null;
            
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null) return NotFound();

            return ItemToDto(todoItem);
        }
        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:long}")]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDTO todoItemDto)
        {
            if (id != todoItemDto.Id)
            {
                return BadRequest();
            }

            if (_context.TodoItems != null)
            {
                var todoItem = await _context.TodoItems.FindAsync(id);
                if (todoItem == null)
                {
                    return NotFound();
                }

                todoItem.Name = todoItemDto.Name;
                todoItem.IsComplete = todoItemDto.IsComplete;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!TodoItemExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }
        // POST: api/TodoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> CreateTodoItem(TodoItemDTO todoItemDto)
        {
            var todoItem = new TodoItem
            {
                IsComplete = todoItemDto.IsComplete,
                Name = todoItemDto.Name
            };

            _context.TodoItems?.Add(todoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetTodoItem),
                new { id = todoItem.Id },
                ItemToDto(todoItem));
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            if (_context.TodoItems != null)
            {
                var todoItem = await _context.TodoItems.FindAsync(id);

                if (todoItem == null)
                {
                    return NotFound();
                }

                _context.TodoItems.Remove(todoItem);
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoItemExists(long id)
        {
            return _context.TodoItems != null && _context.TodoItems.Any(e => e.Id == id);
        }

        private static TodoItemDTO ItemToDto(TodoItem todoItem) =>
            new()
            {
                Id = todoItem.Id,
                Name = todoItem.Name,
                IsComplete = todoItem.IsComplete
            }; 
    }