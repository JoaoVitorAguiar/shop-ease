using Authentication.Infrastructure.Extensions;
using Cart.Infrastructure.Extensions;
using Database.Extensions;
using Presentation.ExceptionHandling;
using Presentation.Routes;
using Products.Infrastructure.Extensions;
using Shared.Mediator;
using Users.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();

builder.Services.AddMediator();
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddUsersModule(builder.Configuration);
builder.Services.AddProductModule(builder.Configuration);
builder.Services.AddCartModule(builder.Configuration);
builder.Services.AddAuthenticationModule(builder.Configuration);
builder.Services.AddExceptionHandling();

builder.Services.AddAuthorization(); 
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

app.UseExceptionHandler();

app.UseAuthentication(); 
app.UseAuthorization(); 

app.MapUserRoutes();
app.MapProductRoutes();
app.MapCartRoutes();


app.Run();