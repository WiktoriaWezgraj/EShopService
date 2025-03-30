using EShop.Application;
//dodac zeby enums sie zamienialo na string

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); 
builder.Services.AddSingleton<ICreditCardService, CreditCardService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); //mozna zautomatyzowac try it out
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
