using Microsoft.AspNetCore.Identity;

namespace ToDoIt.Server.Models;

public sealed class ToDoItUser : IdentityUser<Guid>
{
    public ToDoItUser()
    {
    }

    public ToDoItUser(Guid id, string username, string passwordHash)
    {
        Id = id;
        UserName = username;
        PasswordHash = passwordHash;
    }
}