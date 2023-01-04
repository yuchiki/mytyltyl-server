using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;


WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();

/*
builder.Services
    .AddAuth0WebAppAuthentication(option =>
    {
        option.Domain = builder.Configuration["Auth0:Domain"]!;
        option.ClientId = builder.Configuration["Auth0:ClientID"]!;
    }
    );
*/
builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
                {
                    options.Authority = builder.Configuration["Auth0:Domain"]!;
                    options.Audience = builder.Configuration["Auth0:Audience"];
                    options.RequireHttpsMetadata = false;
                });


WebApplication app = builder.Build();


_ = app.UseDeveloperExceptionPage();


app.UseStaticFiles();

// https://learn.microsoft.com/ja-jp/aspnet/core/host-and-deploy/linux-nginx?tabs=aspnetcore2x&view=aspnetcore-7.0
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedProto
});

app.UseAuthentication();
app.UseAuthorization();
app.MapHealthChecks("/healthz");


if (!app.Environment.IsDevelopment())
{
    _ = app.UseHttpsRedirection();
}


_ = app.UseSwagger();
_ = app.UseSwaggerUI();
_ = app.MapGet("/", (HttpContext context) => context.Response.Redirect("/swagger"));

app.MapControllers();

app.Run();


