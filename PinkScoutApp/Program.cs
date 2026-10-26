using PinkScoutClasslib;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddSingleton<LoginRepository>(serviceProvider =>
{
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("PinkScoutDb")
        ?? throw new InvalidOperationException(
            "The connection string 'PinkScoutDb' is not configured.");
    return new LoginRepository(connectionString);
});

builder.Services.AddSingleton<ActivityRepository>(serviceProvider =>
{
    var initialActivities = new List<Activity>();
    return new ActivityRepository(initialActivities);
});

builder.Services.AddScoped<ActivityService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
