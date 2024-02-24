using System.ComponentModel.DataAnnotations;
using ToDoIt.Server.Models;

namespace ToDoIt.Server.Controllers;

public record CreateToDoRequest(
    [Required(AllowEmptyStrings = false)] string Description,
    [Required] Priority Priority,
    [Required] bool Done
);  