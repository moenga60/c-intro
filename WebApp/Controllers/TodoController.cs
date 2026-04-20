using Microsoft.AspNetCore.Mvc; 
using WebApp.Models;
using WebApp.Data;

namespace  WebApp.Controllers;

[ApiController]
[Route("api/todos")]
public class TodoController : ControllerBase

{
    [HttpGet]
    public IActionResult GetAll()

    {
        return Ok(TodoStore.Todos);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var todo = TodoStore.Todos.FirstOrDefault(t => t.Id == id);
        if (todo == null) return NotFound();
        return Ok(todo);
    }

    [HttpPost]
    public IActionResult Create(Todo newTodo)
    {
        newTodo.Id = TodoStore.Todos.Max(t => t.Id) + 1;
        TodoStore.Todos.Add(newTodo);
        return CreatedAtAction(nameof(GetById), new { id = newTodo.Id }, newTodo);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Todo updatedTodo)
    {
        var todo = TodoStore.Todos.FirstOrDefault( t => t.Id == id);
        if (todo == null) return NotFound();

        todo.Title = updatedTodo.Title;
        todo.IsCompleted = updatedTodo.IsCompleted;
        
        return NoContent();



    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var todo = TodoStore.Todos.FirstOrDefault(t => t.Id == id);
        if (todo == null) return NotFound();

        TodoStore.Todos.Remove(todo);
        return NoContent();
    }
}
