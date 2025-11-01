using Exam.CORE.Interfaces;
using Exam.CORE.Models;

namespace Exam.BLL.Services;
public class OrderService
{
    private readonly IOrderRepository _repository;

    public OrderService(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Order>> GetAllOrdersAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task AddOrderAsync(Order order)
    {
        if (order.Quantity <= 0)
            throw new ArgumentException("Количество должно быть больше 0");

        await _repository.AddAsync(order);
    }

  
    public async Task ConfirmOrderAsync(int orderId)
    {
       
        await Task.CompletedTask;
    }
}