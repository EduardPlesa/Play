using MongoDB.Driver;
using Play.Catalog.Service;
using Play.Catalog.Service.Repositories;
using Play.Catalog.Service.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.SuppressAsyncSuffixInActionNames = false; // (the aspnetcore) not remove the async suffix from action names at runtime
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDatabase(builder.Configuration);

builder.Services.AddSingleton<IItemsRepository, ItemsRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Play.Catalog.Service v1"));
}


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
