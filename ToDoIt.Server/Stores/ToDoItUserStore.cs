using Microsoft.AspNetCore.Identity;
using ToDoIt.Server.Database.Api;
using ToDoIt.Server.Models;

namespace ToDoIt.Server.Stores;

public class ToDoItUserStore(IDatabaseCommandExecutor commandExecutor) : IUserStore<ToDoItUser>
{
    public void Dispose() { }

    public Task<string> GetUserIdAsync(ToDoItUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.Id.ToString());
    }

    public Task<string?> GetUserNameAsync(ToDoItUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.UserName);
    }

    public async Task SetUserNameAsync(ToDoItUser user, string? userName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<string?> GetNormalizedUserNameAsync(ToDoItUser user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task SetNormalizedUserNameAsync(ToDoItUser user, string? normalizedName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<IdentityResult> CreateAsync(ToDoItUser user, CancellationToken cancellationToken)
    {
        return await commandExecutor.Execute<IdentityResult>(async cmd =>
        {
            cmd.AddParam("@id", user.Id);
            cmd.AddParam("@username", user.UserName);
            cmd.AddParam("@password_hash", user.PasswordHash);
            cmd.CommandText = "INSERT INTO users(id, username, password_hash) VALUES(@id, @username, @password_hash);";

            var rowsExecuted = await cmd.ExecuteNonQuery();

            return rowsExecuted == 1 ? IdentityResult.Success : IdentityResult.Failed();
        });
    }

    public Task<IdentityResult> UpdateAsync(ToDoItUser user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IdentityResult> DeleteAsync(ToDoItUser user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<ToDoItUser?> FindByIdAsync(string userId, CancellationToken cancellationToken)
    {
        return await commandExecutor.Execute(async cmd =>
        {
            cmd.CommandText = "SELECT id, username, password_hash FROM users WHERE id = @id;";
            var reader = await cmd.ExecuteQuery();

            if (!await reader.Read())
            {
                // No result found.
                return null;
            }
            
            var id = reader.GetGuid("id");
            var username = reader.Get<string>("username");
            var passwordHash = reader.Get<string>("password_hash");

            return new ToDoItUser(id, username, passwordHash); 
        });
    }

    public Task<ToDoItUser?> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}