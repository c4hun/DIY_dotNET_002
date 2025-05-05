var builder = WebApplication.CreateBuilder(args);

// Ajouter les services n�cessaires pour MVC et les sessions
builder.Services.AddControllersWithViews();

// Ajouter les services de session
builder.Services.AddDistributedMemoryCache();  // Utilisation de la m�moire pour stocker la session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);  // Dur�e de la session
    options.Cookie.HttpOnly = true;  // S�curiser le cookie de session
    options.Cookie.IsEssential = true;  // Permet � la session de fonctionner m�me si les cookies sont d�sactiv�s
});

var app = builder.Build();

// Configurer le pipeline de requ�tes HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

// Activer l'utilisation des sessions dans le pipeline
app.UseSession();  // Ajoute cette ligne pour activer le support des sessions

app.UseRouting();

app.UseAuthorization();

// Configurer les routes MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
