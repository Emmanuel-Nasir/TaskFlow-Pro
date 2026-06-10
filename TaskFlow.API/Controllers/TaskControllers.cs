using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskFlow.API.Data;
using TaskFlow.API.DTOs;
using TaskFlow.API.Models;

namespace TaskFlow.API.Controllers;

//These are called attributes, think of them as sticky notes that tell ASP.NET how to handle this class and its methods.
[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly AppDbContext _db;

    public TasksController(AppDbContext db) => _db = db;

    // GET /api/tasks
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tasks = await _db.Tasks.OrderByDescending(t => t.CreatedAt).ToListAsync();
        return Ok(tasks);
    }

    // GET /api/tasks/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var task = await _db.Tasks.FindAsync(id);
        return task is null ? NotFound() : Ok(task);
    }

    // POST /api/tasks
    [HttpPost]
    public async Task<IActionResult> Create(CreateTaskDto dto)
    {
        //Database expects a TaskItem, that is why we are creating a new instance of TaskItem and populating it with the data from the CreateTaskDto.
        var task = new TaskItem
        {
            Title       = dto.Title,
            Description = dto.Description,
            DueDate     = dto.DueDate,
            Priority    = dto.Priority
        };

        _db.Tasks.Add(task);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
    }

    // PUT /api/tasks/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateTaskDto dto)
    {
        var task = await _db.Tasks.FindAsync(id);
        if (task is null) return NotFound();

        task.Title       = dto.Title;
        task.Description = dto.Description;
        task.DueDate     = dto.DueDate;
        task.IsCompleted = dto.IsCompleted;
        task.Priority    = dto.Priority;

        await _db.SaveChangesAsync();
        return Ok(task);
    }

    // DELETE /api/tasks/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var task = await _db.Tasks.FindAsync(id);
        if (task is null) return NotFound();

        _db.Tasks.Remove(task);
        await _db.SaveChangesAsync();
        return NoContent();
    }

    // PATCH /api/tasks/5/complete
    [HttpPatch("{id}/complete")]
    public async Task<IActionResult> ToggleComplete(int id)
    {
        var task = await _db.Tasks.FindAsync(id);
        if (task is null) return NotFound();

        task.IsCompleted = !task.IsCompleted;
        await _db.SaveChangesAsync();
        return Ok(task);
    }
}