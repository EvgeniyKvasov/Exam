using Exam.BLL.Services;
using Exam.CORE.Interfaces;
using Exam.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Получаем connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Репозитории с прямым Npgsql
builder.Services.AddScoped<IProductRepository>(_ => new ProductRepository(connectionString));
builder.Services.AddScoped<IOrderRepository>(_ => new OrderRepository(connectionString));

// Сервисы бизнес-логики
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<OrderService>();

// API
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();