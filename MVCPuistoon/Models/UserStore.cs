using System;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using MVCPuistoon.Models;

namespace MVCPuistoon.Models
{
    public class UserStore : IUserStore<Käyttäjä>, IUserPasswordStore<Käyttäjä>
    {
        public static DbConnection GetOpenConnection()
        {
            var connection = new SqlConnection(""); connection.Open(); return connection;
           
        }
        public async Task<IdentityResult> CreateAsync(Käyttäjä user, CancellationToken cancellationToken)
        {
            using (var connection = GetOpenConnection())
            {
                await connection.ExecuteAsync(
                    "insert into Käyttäjä([Id]," +
                    "[KäyttäjäTunnus]," +
                    "[NormalizedUserName]," +
                    "[PasswordHash]) " +
                    "Values(@id,@userName,@normalizedUserName,@passwordHash)",
                    new
                    {
                        id = user.Id,
                        userName = user.KäyttäjäTunnus,
                        normalizedUserName = user.NormalizedUserName,
                        passwordHash = user.PasswordHash
                    }
                );
            }
            return IdentityResult.Success;
        }
        public async Task<IdentityResult> UpdateAsync(Käyttäjä user, CancellationToken cancellationToken)
        {
            using (var connection = GetOpenConnection())
            {
                await connection.ExecuteAsync(
                    "update Käyttäjä " +
                    "set [Id] = @id," +
                    "[KäyttäjäTunnus] = @userName," +
                     "[NormalizedUserName] = @normalizedUserName," +
                    "[PasswordHash] = @passwordHash " +
                    "where [Id] = @id",
                    new
                    {
                        id = user.Id,
                        userName = user.KäyttäjäTunnus,
                        normalizedUserName = user.NormalizedUserName,
                        passwordHash = user.PasswordHash
                    }
                );
            }
            return IdentityResult.Success;
        }
        public async Task<Käyttäjät> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            using (var connection = GetOpenConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<Käyttäjät>(
                    "select * From Käyttäjä where Id = @id",
                    new { id = userId });
            }
        }
        public Task SetPasswordHashAsync(Käyttäjä user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.CompletedTask;
        }
        public Task<string> GetPasswordHashAsync(Käyttäjä user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }
        public Task<bool> HasPasswordAsync(Käyttäjä user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash != null);
        }
        public Task<IdentityResult> DeleteAsync(Käyttäjä user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public void Dispose()
        { }
        public async Task<Käyttäjä> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            using (var connection = GetOpenConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<Käyttäjä>(
                    "select * From Käyttäjä where NormalizedUserName = @name",
                    new { name = normalizedUserName });
            }
        }
        public Task<string> GetNormalizedUserNameAsync(Käyttäjä user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedUserName);
        }
        public Task<string> GetUserIdAsync(Käyttäjä user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id);
        }
        public Task<string> GetUserNameAsync(Käyttäjä user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.KäyttäjäTunnus);
        }
        public Task SetNormalizedUserNameAsync(Käyttäjä user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
            return Task.CompletedTask;
        }
        public Task SetUserNameAsync(Käyttäjä user, string userName, CancellationToken cancellationToken)
        {
            user.KäyttäjäTunnus = userName;
            return Task.CompletedTask;
        }
        async Task<Käyttäjä> IUserStore<Käyttäjä>.FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            using (var connection = GetOpenConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<Käyttäjä>(
                    "select * From Käyttäjä where Id = @id",
                   new { id = userId });
            }
        }
    }
}

       
      
       
        
    