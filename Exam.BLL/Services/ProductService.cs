using Exam.CORE.Interfaces;
using Exam.CORE.Models;

namespace Exam.BLL.Services;
public class ProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task AddProductAsync(Product product)
    {
        if (product.Price <= 0)
            throw new ArgumentException("Цена должна быть больше 0");

        await _repository.AddAsync(product);
    }

    public async Task UpdateProductAsync(Product product)
    {
        if (product.Price <= 0)
            throw new ArgumentException("Цена должна быть больше 0");

        await _repository.UpdateAsync(product);
    }

    public async Task DeleteProductAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}