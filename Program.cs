using Challenge.Data.Context;
using Challenge.Hubs;
using Challenge.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ChallengeDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddSignalR(conf => conf.EnableDetailedErrors = true);
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<INoteRepository, NoteRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(
    options =>
        options.AddPolicy("AllowAll", b => b.WithOrigins("https://localhost:7113", "http://localhost:5185")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials())
);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowAll");
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<CallCenterHub>("/callcenter");
    endpoints.MapHub<NoteCallHubCenter>("/notecallcenter");
    endpoints.MapHub<AdminCallCenterHub>("admincallcenter");
}
);
app.MapControllers();

app.Run();
