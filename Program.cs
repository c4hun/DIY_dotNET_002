var builder = WebApplication.CreateBuilder(args);

// Ajouter les services nécessaires pour MVC et les sessions
builder.Services.AddControllersWithViews();

// Ajouter les services de session
builder.Services.AddDistributedMemoryCache();  // Utilisation de la mémoire pour stocker la session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);  // Durée de la session
    options.Cookie.HttpOnly = true;  // Sécuriser le cookie de session
    options.Cookie.IsEssential = true;  // Permet à la session de fonctionner même si les cookies sont désactivés
});

var app = builder.Build();

// Configurer le pipeline de requêtes HTTP
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
