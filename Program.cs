using FluentValidation;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using ThePlannerAPI.Context;
using ThePlannerAPI.Interface;
using ThePlannerAPI.Services;
using ThePlannerAPI.Shared;
using ThePlannerAPI.Validators.Assignment;
using ThePlannerAPI.Validators.Category;
using ThePlannerAPI.Validators.User;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IUser, UserService>();
builder.Services.AddScoped<ICategory, CategoryService>();
builder.Services.AddScoped<IAssignment, AssignmentService>();
builder.Services.AddScoped<IAssignmentUser, AssignmentUsersService>();

builder.Services.AddScoped<AssignmentValidationQuery>();
builder.Services.AddScoped<CategoryValidationQuery>();
builder.Services.AddScoped<UserValidationQuery>();

builder.Services.AddScoped<FillterAssignmentsValidator>();
builder.Services.AddScoped<UpdateAssignmentsValidator>();
builder.Services.AddScoped<AddAssignmentsValidator>();

builder.Services.AddScoped<AddUserValidation>();
builder.Services.AddScoped<UpdateUserValidator>();


builder.Services.AddScoped<CategoryValidator>();

// Add services to the container.
// Example ApplicationDbContext registration
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")),
    ServiceLifetime.Scoped
);

// Register validators
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.Services.AddControllers();

builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API Title", Version = "v1" });
});


// Enable CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:3000") // Add your frontend URL here
                      .AllowAnyHeader()
                      .AllowAnyMethod();
    });
});

var app = builder.Build();




// ... other middleware registrations

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    // Enable Swagger UI in development mode
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API Title V1"));
}

app.UseCors();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
