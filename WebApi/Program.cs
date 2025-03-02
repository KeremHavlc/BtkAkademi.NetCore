using Microsoft.EntityFrameworkCore;
using WebApi.Repositories;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
//RepositoryContext sýnýfý IoC Container'a ekleniyor.
builder.Services.AddDbContext<RepositoryContext>(options =>
//Sql Server Connection String'i appsettings.json dosyasýndan alýnýyor.    
options.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection")));

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
