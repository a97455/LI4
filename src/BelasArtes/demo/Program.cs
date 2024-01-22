using DataLayer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient();
builder.Services.AddTransient<ISqlDataAccess, SqlDataAccess>();
builder.Services.AddTransient<ILeilaoDAO, LeilaoDAO>();
builder.Services.AddTransient<ILicitacaoDAO, LicitacaoDAO>();
builder.Services.AddTransient<IPinturaDAO, PinturaDAO>();
builder.Services.AddTransient<IUtilizadorDAO, UtilizadorDAO>();
builder.Services.AddSingleton<AppState>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}


app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
