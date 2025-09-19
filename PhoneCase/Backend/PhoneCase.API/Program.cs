using System.Text;
using PhoneCase.API;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGeneralServices();
builder.Services.AddConfigurations();
builder.Services.AddContextServices();
builder.Services.AddIdentityServices();
builder.Services.AddAuthenticationServices();
builder.Services.AddRepositories();
builder.Services.AddManagers();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();