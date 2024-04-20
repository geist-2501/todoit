using Microsoft.AspNetCore.Identity;
using ToDoIt.Server.Database.Api;
using ToDoIt.Server.Logging;
using ToDoIt.Server.Models;
using ILogger = Serilog.ILogger;

namespace ToDoIt.Server.Stores;

public class ToDoItUserStore(IDatabaseCommandExecutor commandExecutor) : IUserEmailStore<ToDoItUser>, IUserPasswordStore<ToDoItUser>
{
    private static readonly ILogger s_Logger = LogManager.GetLogger(nameof(ToDoItUserStore)); 
    
    public async Task<IdentityResult> CreateAsync(ToDoItUser user, CancellationToken cancellationToken)
    {
        return await commandExecutor.Execute<IdentityResult>(async cmd =>
        {
            cmd.AddParam("@id", user.Id);
            cmd.AddParam("@username", user.UserName);
            cmd.AddParam("@email", user.Email);
            cmd.AddParam("@password_hash", user.PasswordHash);
            cmd.CommandText = 
                """
                INSERT INTO users(id, username, email, password_hash, email_confirmed) 
                VALUES(@id, @username, @email, @password_hash, false);
                """;

            var rowsExecuted = await cmd.ExecuteNonQuery();

            return rowsExecuted == 1 ? IdentityResult.Success : IdentityResult.Failed();
        });
    }

    public async Task<IdentityResult> UpdateAsync(ToDoItUser user, CancellationToken cancellationToken)
    {
        return await commandExecutor.Execute<IdentityResult>(async cmd =>
        {
            cmd.AddParam("@id", user.Id);
            cmd.AddParam("@username", user.UserName);
            cmd.AddParam("@email", user.Email);
            cmd.AddParam("@password_hash", user.PasswordHash);
            cmd.AddParam("@email_confirmed", user.EmailConfirmed);
            cmd.CommandText =
                """
                UPDATE users 
                SET (email, username, password_hash, email_confirmed) = (@email, @username, @password_hash, @email_confirmed) 
                WHERE id = @id
                """;

            var rowsExecuted = await cmd.ExecuteNonQuery();

            return rowsExecuted == 1 ? IdentityResult.Success : IdentityResult.Failed();
        });
    }

    public async Task<IdentityResult> DeleteAsync(ToDoItUser user, CancellationToken cancellationToken)
    {
        return await commandExecutor.Execute<IdentityResult>(async cmd =>
        {
            cmd.AddParam("@id", user.Id);
            cmd.CommandText =
                """
                DELETE FROM users
                WHERE id = @id;
                """;

            var rowsExecuted = await cmd.ExecuteNonQuery();

            return rowsExecuted == 1 ? IdentityResult.Success : IdentityResult.Failed();
        });
    }
    
    public async Task<ToDoItUser?> FindByIdAsync(string userId, CancellationToken cancellationToken)
    {
        return await commandExecutor.Execute(async cmd =>
        {
            var userGuid = Guid.Parse(userId);
            cmd.AddParam("@id", userGuid);
            cmd.CommandText = "SELECT id, username, email, password_hash, email_confirmed FROM users WHERE id = @id;";
            var reader = await cmd.ExecuteQuery();

            if (!await reader.Read())
            {
                // No result found.
                return null;
            }
            
            return MakeUser(reader);
        });
    }

    public async Task<ToDoItUser?> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
    {
        var email = normalizedEmail.ToLower();
        s_Logger.Information("FindByEmailAsync {Email}", email);
        return await commandExecutor.Execute(async cmd =>
        {
            cmd.AddParam("@email", email);
            cmd.CommandText = "SELECT id, username, email, password_hash, email_confirmed FROM users WHERE email = @email;";
            var reader = await cmd.ExecuteQuery();

            if (!await reader.Read())
            {
                // No result found.
                return null;
            }

            return MakeUser(reader);
        });
    }
    

    public Task<string> GetUserIdAsync(ToDoItUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.Id.ToString());
    }

    public Task<string?> GetUserNameAsync(ToDoItUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.UserName);
    }

    public Task SetUserNameAsync(ToDoItUser user, string? userName, CancellationToken cancellationToken)
    {
        user.UserName = userName;
        return Task.CompletedTask;
    }

    public Task<string?> GetNormalizedUserNameAsync(ToDoItUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.Email?.ToLower());
    }

    public Task SetNormalizedUserNameAsync(ToDoItUser user, string? normalizedName, CancellationToken cancellationToken)
    {
        // No-op.
        return Task.CompletedTask;
    }

    public async Task<ToDoItUser?> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
    {
        return await FindByEmailAsync(normalizedUserName, cancellationToken);
    }

    public Task SetEmailAsync(ToDoItUser user, string? email, CancellationToken cancellationToken)
    {
        user.Email = email?.ToLower();
        return Task.CompletedTask;
    }

    public Task<string?> GetEmailAsync(ToDoItUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.Email);
    }

    public Task<bool> GetEmailConfirmedAsync(ToDoItUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.EmailConfirmed);
    }

    public Task SetEmailConfirmedAsync(ToDoItUser user, bool confirmed, CancellationToken cancellationToken)
    {
        user.EmailConfirmed = confirmed;
        return Task.CompletedTask;
    }

    public Task<string?> GetNormalizedEmailAsync(ToDoItUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.Email?.ToLower());
    }

    public Task SetNormalizedEmailAsync(ToDoItUser user, string? normalizedEmail, CancellationToken cancellationToken)
    {
        // No-op.
        return Task.CompletedTask;
    }

    public Task SetPasswordHashAsync(ToDoItUser user, string? passwordHash, CancellationToken cancellationToken)
    {
        user.PasswordHash = passwordHash;
        return Task.CompletedTask;
    }

    public Task<string?> GetPasswordHashAsync(ToDoItUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.PasswordHash);
    }

    public Task<bool> HasPasswordAsync(ToDoItUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(true);
    }
    
    private static ToDoItUser MakeUser(IDatabaseReader reader)
    {
        var id = reader.GetGuid("id");
        var email = reader.Get<string>("email");
        var username = reader.Get<string>("username");
        var passwordHash = reader.Get<string>("password_hash");
        var emailConfirmed = reader.Get<bool>("email_confirmed");

        return new ToDoItUser(id, username, email, passwordHash, emailConfirmed);
    }
    
    public void Dispose() { }
}