using Trail_Milestone2.IRepo;
using Trail_Milestone2.IService;
using Trail_Milestone2.Repo;
using Trail_Milestone2.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("Connection");

builder.Services.AddSingleton<IMotorbikeRepo>(provider => new MotorbikeRepo(connectionString));
builder.Services.AddScoped<IMotorbikeService,MotorbikeService>();

builder.Services.AddSingleton<ICustomer_PageRepo>(prvider => new Customer_PageRepo(connectionString));
builder.Services.AddScoped<ICustomer_PageService,Customer_PageService>();

builder.Services.AddControllers();
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

app.UseStaticFiles(); //This serves static files like the uploaded images



app.UseRouting();

app.UseHttpsRedirection();

app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());



app.UseAuthorization();

app.MapControllers();

app.Run();
