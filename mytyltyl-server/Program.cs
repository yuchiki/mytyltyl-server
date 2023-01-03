using Auth0.AspNetCore.Authentication;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddAuth0WebAppAuthentication(option =>
    {
        option.Domain = builder.Configuration["Auth0:Domain"]!;
        option.ClientId = builder.Configuration["Auth0:ClientID"]!;
    }
    );

WebApplication app = builder.Build();


_ = app.UseDeveloperExceptionPage();


app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();


if (app.Environment.IsDevelopment()) { }


_ = app.UseSwagger();
_ = app.UseSwaggerUI();
_ = app.MapGet("/", (HttpContext context) => context.Response.Redirect("/swagger"));

app.MapControllers();

app.Run();


