using Exam.CORE.Interfaces;
using Exam.CORE.Models;
using Npgsql;

namespace Exam.DAL.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly string _connectionString;
    public OrderRepository(string connectionString) => _connectionString = connectionString;

    public async Task<List<Order>> GetAllAsync()
    {
        var orders = new List<Order>();
        using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        using var cmd = new NpgsqlCommand(@"
            SELECT o.*, u.name as username, u.email, p.name as productname, p.price
            FROM orders o
            INNER JOIN users u ON o.userid = u.id
            INNER JOIN products p ON o.productid = p.id", connection);

        using var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            orders.Add(new Order
            {
                Id = reader.GetInt32(0),
                UserId = reader.GetInt32(1),
                ProductId = reader.GetInt32(2),
                Quantity = reader.GetInt32(3),
                OrderDate = reader.GetDateTime(4),
              
                IsConfirmed = false,
                User = new User { Id = reader.GetInt32(1), Name = reader.GetString(5), Email = reader.GetString(6) },
                Product = new Product { Id = reader.GetInt32(2), Name = reader.GetString(7), Price = reader.GetDecimal(8) }
            });
        }
        return orders;
    }

    public async Task<Order?> GetByIdAsync(int id)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        using var cmd = new NpgsqlCommand(@"
            SELECT o.*, u.name as username, u.email, p.name as productname, p.price
            FROM orders o
            INNER JOIN users u ON o.userid = u.id
            INNER JOIN products p ON o.productid = p.id
            WHERE o.id = @id", connection);
        cmd.Parameters.AddWithValue("@id", id);

        using var reader = await cmd.ExecuteReaderAsync();
        return await reader.ReadAsync() ? new Order
        {
            Id = reader.GetInt32(0),
            UserId = reader.GetInt32(1),
            ProductId = reader.GetInt32(2),
            Quantity = reader.GetInt32(3),
            OrderDate = reader.GetDateTime(4),
            
            IsConfirmed = false,
            User = new User { Id = reader.GetInt32(1), Name = reader.GetString(5), Email = reader.GetString(6) },
            Product = new Product { Id = reader.GetInt32(2), Name = reader.GetString(7), Price = reader.GetDecimal(8) }
        } : null;
    }

    public async Task AddAsync(Order order)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        using var cmd = new NpgsqlCommand(
            "INSERT INTO orders (userid, productid, quantity, orderdate) VALUES (@userid, @productid, @quantity, @orderdate) RETURNING id",
            connection);
        cmd.Parameters.AddWithValue("@userid", order.UserId);
        cmd.Parameters.AddWithValue("@productid", order.ProductId);
        cmd.Parameters.AddWithValue("@quantity", order.Quantity);
        cmd.Parameters.AddWithValue("@orderdate", order.OrderDate);

        order.Id = (int)(await cmd.ExecuteScalarAsync())!;
    }

   
    public async Task UpdateAsync(Order order)
    {
        
        await Task.CompletedTask;
    }
}