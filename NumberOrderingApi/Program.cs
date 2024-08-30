using NumberOrderingApi.Data.Repositories;
using NumberOrderingApi.Services;
using NumberOrderingApi.Services.Sorting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddTransient<INumbersRepository, TxtNumbersRepository>(_ =>
{
    return new TxtNumbersRepository("Data");
});
builder.Services.AddTransient<ISortingService, BubbleSortService>();
builder.Services.AddTransient<INumberOrderingService, NumberOrderingService>();
builder.Services.AddTransient<INumberValidationService, NumberValidationService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
