using WebApp.Models;

namespace WebApp.Data;

public static class TodoStore
{
    public static List<Todo> Todos = new List<Todo>
    {
        new Todo { Id = 1, Title = "Learn .NET", IsCompleted = false },
        new Todo { Id = 2, Title = "Build API", IsCompleted = false }
    };
}