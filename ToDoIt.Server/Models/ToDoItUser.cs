namespace ToDoIt.Server.Models;

public sealed class ToDoItUser(Guid id, string? username, string? email, string? passwordHash, bool emailConfirmed)
{
    public Guid Id { get; } = id;
    public string? UserName { get; set; } = username;
    public string? Email { get; set; } = email;
    public string? PasswordHash { get; set; } = passwordHash;

    public bool EmailConfirmed { get; set; } = emailConfirmed;

    public ToDoItUser() : this(Guid.NewGuid(), null, null, null, false) {}
}