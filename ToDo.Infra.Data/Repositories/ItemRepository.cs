using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;
using ToDo.Domain.Entities;
using ToDo.Domain.Interface;

namespace ToDo.Infra.Data.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly string connectionString;
        public ItemRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("ToDoDb");
        }

        public async Task<IEnumerable<Item>> GetAllAsync()
        {
            IEnumerable<Item> result;
            var query = "select * from Items";
            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    result = await con.QueryAsync<Item>(query);
                }
                catch(Exception e)
                {
                    await con.ExecuteAsync("create table Items( Id uniqueidentifier not null primary key, Description varchar(256) not null, Done bit not null, CreatedAt Datetime not null);");
                    return await GetAllAsync();
                }
                finally
                {
                    con.Close();
                }
                return result;
            }
        }
        
        public async Task<Item> GetAsync(Guid id)
        {
            Item result;
            var query = "select * from Items where Id = '" + id +"'";
            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    result = await con.QueryFirstAsync<Item>(query);
                }
                catch(Exception e)
                {
                    throw e;
                }
                finally
                {
                    con.Close();
                }
                return result;
            }
        }

        public async Task AddAsync(Item item)
        {
            var query = "insert into Items(Id, Description, Done, CreatedAt) values(@Id, @Description, @Done, @CreatedAt)";
            using (var con = new SqlConnection(connectionString))
            {    
                try
                {
                    con.Open();
                    await con.ExecuteAsync(query, item);
                }
                finally
                {
                    con.Close();
                }
            }
        }

        public async Task EditAsync(Item item)
        {
            var count = 0;
            var query = "update Items set Description = @Description, Done = @Done where Id = @Id";
            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    count = await con.ExecuteAsync(query, item);
                }
                finally
                {
                    con.Close();
                }
            }
        }
        
        public async Task DeleteAsync(Guid id)
        {
            var query = "delete from Items where Id = '" + id + "'";
            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    await con.ExecuteAsync(query);
                }
                catch(Exception e)
                {
                    throw e;
                }
                finally
                {
                    con.Close();
                }
            }
        }
    }
}
