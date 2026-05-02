using Deliveroo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// AGGIUNTA PER RESTITUIRE LA SESSIONE
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(options =>
    {
        options.IdleTimeout = TimeSpan.FromMinutes(10);
    }
);

//Registro le classi GestioneDati e GestioneCarrello cosi da poterli usare all'interno dei .cshtml
builder.Services.AddScoped<GestioneDati>();
//builder.Services.AddScoped<GestioneCarrello>();

builder.Services.AddSingleton<CloudinaryService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

//aggiunta per la sessione
app.UseSession();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();