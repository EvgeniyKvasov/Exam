using Exam.BLL.Services;
using Exam.CORE.Interfaces;
using Exam.DAL.Repositories;
using Exam.WEB.Components;

var builder = WebApplication.CreateBuilder(args);

// Получаем connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Репозитории с прямым Npgsql
builder.Services.AddScoped<IProductRepository>(_ => new ProductRepository(connectionString));
builder.Services.AddScoped<IOrderRepository>(_ => new OrderRepository(connectionString));

// Сервисы бизнес-логики
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<OrderService>();

// Blazor Server
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();