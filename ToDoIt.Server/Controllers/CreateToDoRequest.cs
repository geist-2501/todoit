using ToDoIt.Server.Models;

namespace ToDoIt.Server.Controllers;

public class CreateToDoRequest
{
    public string Description { get; set; }
    public Priority Priority { get; set; }
}  