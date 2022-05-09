using poc.dapper_repository.Domain.Entities;
using poc.dapper_repository.Infra.Context;
using Dapper;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using poc.dapper_repository.Domain.interfaces;

namespace poc.dapper_repository.Infra.Repositories;

public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
{
    private readonly DbConext _dbConext;
    private readonly string _tableName;

    private IEnumerable<PropertyInfo> GetProperties => typeof(TEntity).GetProperties();

    public Repository(DbConext context, string tableName)
    {
        _dbConext = context;
        _tableName = tableName;
    }

    public async Task<bool> Add(TEntity entity)
    {
        var query = GenerateInsertQuery();

        using (var connection = await _dbConext.CreateConnectionAsync())
        {
            return await connection.ExecuteAsync(query, entity) > 0;
        }
        
    }

    public async Task<IEnumerable<TEntity>> GetAll()
    {
        using (var connection = await _dbConext.CreateConnectionAsync())
        {
            return await connection.QueryAsync<TEntity>($"SELECT * FROM {_tableName} ");
        }
    }

    public async Task<TEntity?> GetById(Guid id)
    {
        using (var connection = await _dbConext.CreateConnectionAsync())
        {
            return await connection.QuerySingleOrDefaultAsync($"SELECT * FROM {_tableName} WHERE Id=@Id", new { Id = id });
        }
    }

    public async Task<bool> Remove(Guid id)
    {
        using (var connection = await _dbConext.CreateConnectionAsync())
        {
            return await connection.ExecuteAsync($"DELETE FROM {_tableName} WHERE Id=@Id", new { Id = id }) > 0;
        }
    }

    public async Task<bool> Update(TEntity newEntity, Guid? id)
    {
        var query = GenerateUpdateQuery(id);
        using (var connection = await _dbConext.CreateConnectionAsync())
        {
            return await connection.ExecuteAsync(query, newEntity) > 0;
        }
    }

    private static List<string> GenerateListOfProperties(IEnumerable<PropertyInfo> listOfProperties)
    {

        return (from prop in listOfProperties
                let attributes = prop.GetCustomAttributes(typeof(DescriptionAttribute), false)
                where attributes.Length <= 0 || (attributes[0] as DescriptionAttribute)?.Description != "ignore"
                select prop.Name).ToList();
    }

    private string GenerateInsertQuery()
    {
        var insertQuery = new StringBuilder($"INSERT INTO {_tableName} ");

        insertQuery.Append("(");

        var properties = GenerateListOfProperties(GetProperties);
        properties.ForEach(prop => { insertQuery.Append($"[{prop}],"); });

        insertQuery
            .Remove(insertQuery.Length - 1, 1)
            .Append(") VALUES (");

        properties.ForEach(prop => { insertQuery.Append($"@{prop},"); });

        insertQuery
            .Remove(insertQuery.Length - 1, 1)
            .Append(")");

        return insertQuery.ToString();
    }

    private string GenerateUpdateQuery(Guid? id)
    {
        var updateQuery = new StringBuilder($"UPDATE {_tableName} SET ");
        var properties = GenerateListOfProperties(GetProperties);

        properties.ForEach(property =>
        {
            if (!property.Equals("Id"))
            {
                updateQuery.Append($"{property}=@{property},");
            }
        });

        updateQuery.Remove(updateQuery.Length - 1, 1);
        if (id != null)
            updateQuery.Append($" WHERE Id={id}");
        else
            updateQuery.Append(" WHERE Id=@Id");

        return updateQuery.ToString();
    }
}
