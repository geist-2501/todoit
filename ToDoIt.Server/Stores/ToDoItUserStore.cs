using Microsoft.AspNetCore.Identity;
using ToDoIt.Server.Database.Api;
using ToDoIt.Server.Models;

namespace ToDoIt.Server.Stores;

public class ToDoItUserStore(IDatabaseCommandExecutor commandExecutor) : IUserEmailStore<ToDoItUser>, IUserPasswordStore<ToDoItUser>
{
    public void Dispose() { }

    public Task<string> GetUserIdAsync(ToDoItUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.Id.ToString());
    }

    public Task<string?> GetUserNameAsync(ToDoItUser user, CancellationToken cancellationToken)
    {
        return GetEmailAsync(user, cancellationToken);
    }

    public async Task SetUserNameAsync(ToDoItUser user, string? userName, CancellationToken cancellationToken)
    {
        await SetEmailAsync(user, userName, cancellationToken);
    }

    public Task<string?> GetNormalizedUserNameAsync(ToDoItUser user, CancellationToken cancellationToken)
    {
        return GetNormalizedEmailAsync(user, cancellationToken);
    }

    public async Task SetNormalizedUserNameAsync(ToDoItUser user, string? normalizedName, CancellationToken cancellationToken)
    {
        await SetNormalizedEmailAsync(user, normalizedName, cancellationToken);
    }

    public async Task<IdentityResult> CreateAsync(ToDoItUser user, CancellationToken cancellationToken)
    {
        return await commandExecutor.Execute<IdentityResult>(async cmd =>
        {
            cmd.AddParam("@id", user.Id);
            cmd.AddParam("@email", user.Email);
            cmd.AddParam("@password_hash", user.PasswordHash);
            cmd.CommandText = 
                """
                INSERT INTO users(id, email, password_hash, email_confirmed) 
                VALUES(@id, @username, @password_hash, false);
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
            cmd.AddParam("@email", user.Email);
            cmd.AddParam("@password_hash", user.PasswordHash);
            cmd.AddParam("@email_confirmed", user.EmailConfirmed);
            cmd.CommandText =
                """
                UPDATE users 
                SET (email, password_hash, email_confirmed) = (@email, @password_hash, @email_confirmed) 
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
            cmd.CommandText = "SELECT id, email, password_hash, email_confirmed FROM users WHERE id = @id;";
            var reader = await cmd.ExecuteQuery();

            if (!await reader.Read())
            {
                // No result found.
                return null;
            }
            
            var id = reader.GetGuid("id");
            var email = reader.Get<string>("email");
            var passwordHash = reader.Get<string>("password_hash");
            var emailConfirmed = reader.Get<bool>("email_confirmed");

            return new ToDoItUser(id, email, passwordHash, emailConfirmed); 
        });
    }

    public Task<ToDoItUser?> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
    {
        return FindByEmailAsync(normalizedUserName, cancellationToken);
    }

    public async Task SetEmailAsync(ToDoItUser user, string? email, CancellationToken cancellationToken)
    {
        await commandExecutor.Execute(async cmd =>
        {
            cmd.AddParam("@id", user.Id);
            cmd.AddParam("@email", email ?? throw new ArgumentNullException(nameof(email), "Trying to set email to null"));
            cmd.CommandText =
                """
                UPDATE users
                SET email = @email
                WHERE id = @id;
                """;

            await cmd.ExecuteNonQuery();
        });
    }

    public Task<string?> GetEmailAsync(ToDoItUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult<string?>(user.Email);
    }

    public Task<bool> GetEmailConfirmedAsync(ToDoItUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.EmailConfirmed);
    }

    public async Task SetEmailConfirmedAsync(ToDoItUser user, bool confirmed, CancellationToken cancellationToken)
    {
        await commandExecutor.Execute(async cmd =>
        {
            cmd.AddParam("@id", user.Id);
            cmd.AddParam("@email_confirmed", confirmed);
            cmd.CommandText =
                """
                UPDATE users
                SET email_confirmed = @email_confirmed
                WHERE id = @id;
                """;

            await cmd.ExecuteNonQuery();
        });
    }

    public async Task<ToDoItUser?> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
    {
        return await commandExecutor.Execute(async cmd =>
        {
            cmd.CommandText = "SELECT id, email, password_hash, email_confirmed FROM users WHERE email = @email;";
            var reader = await cmd.ExecuteQuery();

            if (!await reader.Read())
            {
                // No result found.
                return null;
            }
            
            var id = reader.GetGuid("id");
            var email = reader.Get<string>("email");
            var passwordHash = reader.Get<string>("password_hash");
            var emailConfirmed = reader.Get<bool>("email_confirmed");

            return new ToDoItUser(id, email, passwordHash, emailConfirmed); 
        });
    }

    public async Task<string?> GetNormalizedEmailAsync(ToDoItUser user, CancellationToken cancellationToken)
    {
        return await GetEmailAsync(user, cancellationToken);
    }

    public async Task SetNormalizedEmailAsync(ToDoItUser user, string? normalizedEmail, CancellationToken cancellationToken)
    {
        await SetEmailAsync(user, normalizedEmail, cancellationToken);
    }

    public async Task SetPasswordHashAsync(ToDoItUser user, string? passwordHash, CancellationToken cancellationToken)
    {
        await commandExecutor.Execute(async cmd =>
        {
            cmd.AddParam("@id", user.Id);
            cmd.AddParam("@password_hash", passwordHash ?? throw new ArgumentNullException(nameof(passwordHash), "Trying to set password hash to null"));
            cmd.CommandText =
                """
                UPDATE users
                SET password_hash = @password_hash
                WHERE id = @id;
                """;

            await cmd.ExecuteNonQuery();
        });
    }

    public Task<string?> GetPasswordHashAsync(ToDoItUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult<string?>(user.PasswordHash);
    }

    public Task<bool> HasPasswordAsync(ToDoItUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(true);
    }
}