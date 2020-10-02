using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using Todo_List_Project.Data;
using Todo_List_Project.Dtos;
using Todo_List_Project.Models;

namespace Todo_List_Project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<TodoController> _logger;
        private readonly DataContext _context;

        public TodoController(ILogger<TodoController> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<List<Todo>> Get()
        {
            return await _context.Todos.ToListAsync();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateNew([FromBody] TodoDto todo)
        {
            if (ModelState.IsValid)
            {
                var count = await _context.Todos.CountAsync();
                var newTask = new Todo()
                {
                    Id = count + 1,
                    Title = todo.Title,
                    State = TodoState.Todo.ToString(),
                    isCompleted = false
                };

                await _context.Todos.AddAsync(newTask);
                await _context.SaveChangesAsync();

                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("ChangeTaskState")]
        public async Task<IActionResult> ChangeTaskState([FromBody] TodoDto task)
        {
            if (task.Id != 0 && !String.IsNullOrEmpty(task.Title) && !String.IsNullOrEmpty(task.State))
            {
                if (await _context.Todos.AnyAsync(t => t.Id == task.Id))
                {
                    var updatedTask = new Todo()
                    {
                        Id = task.Id,
                        Title = task.Title,
                        State = task.State,
                        isCompleted = task.State == TodoState.Completed.ToString()
                    };
                    _context.Update(updatedTask);
                    await _context.SaveChangesAsync();

                    return Ok();
                }
                else
                {
                    return BadRequest("Task not found");
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("DeleteTask")]
        public async Task<IActionResult> DeleteTask([FromBody] TodoDto task)
        {
            if (await _context.Todos.AnyAsync(t => t.Id == task.Id))
            {
                var taskToRemove = await _context.Todos.FirstOrDefaultAsync(t => t.Id == task.Id);

                _context.Todos.Remove(taskToRemove);
                await _context.SaveChangesAsync();

                return Ok();
            }
            else
            {
                return BadRequest("Task not found");
            }
        }
    }
}
