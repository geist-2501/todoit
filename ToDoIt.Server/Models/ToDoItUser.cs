namespace ToDoIt.Server.Models;

public sealed class ToDoItUser(Guid id, string email, string passwordHash, bool emailConfirmed)
{
    public Guid Id { get; } = id;
    public string Email { get; } = email;
    public string PasswordHash { get; } = passwordHash;

    public bool EmailConfirmed { get; } = emailConfirmed;

    public ToDoItUser() : this(Guid.NewGuid(), "beep", "beep", false) {}
}