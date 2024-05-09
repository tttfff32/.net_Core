using lesson_2;
using lesson_2.Utilties;
using Middlewares;
using lesson_2.Services;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;

internal partial class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        // builder.Services.AddSwaggerGen();
        builder.Services.AddListToDo();
        builder.Logging.ClearProviders();
        builder.Logging.AddConsole();



        builder.Services.AddAuthentication(options =>
      {
          options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
      })
      .AddJwtBearer(cfg =>
      {
          cfg.RequireHttpsMetadata = false;
          cfg.TokenValidationParameters = TokenService.GetTokenValidationParameters();
      });

        builder.Services.AddAuthorization(cfg =>
          {
              cfg.AddPolicy("Admin", policy => policy.RequireClaim("type", "Admin"));
              cfg.AddPolicy("classUser", policy => policy.RequireClaim("type", "classUser"));
          });

         builder.Services.AddControllers();
        builder.Services.AddSwaggerGen(c =>
           {
               c.SwaggerDoc("v1", new OpenApiInfo { Title = "ToDoList", Version = "v1" });
               c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
               {
                   In = ParameterLocation.Header,
                   Description = "Please enter JWT with Bearer into field",
                   Name = "Authorization",
                   Type = SecuritySchemeType.ApiKey
               });
               c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                { new OpenApiSecurityScheme
                        {
                         Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer"}
                        },
                    new string[] {}
                }
               });
           });
    

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ToDoList v1"));
        }
app.UseMyLogMiddleware("E:/שיעורי בית תשפד/זילברברג/.net_Core/lesson-2/Log.txt");
app.UseHttpsRedirection();

app.UseDefaultFiles();
app.UseStaticFiles();


app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
    }
}