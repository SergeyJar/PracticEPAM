using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using PracticEPAM.Models;
using PracticEPAM.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DbContextConnection") ?? throw new InvalidOperationException("Connection string 'Connection' not found.");
builder.Services.AddDbContext<DataBaseSiteContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDefaultIdentity<IdentityUser>
(options => options.SignIn.RequireConfirmedAccount = true
).AddRoles<IdentityRole>().AddRoleManager<RoleManager<IdentityRole>>().AddEntityFrameworkStores<DataBaseSiteContext>();
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration);
builder.Services.AddControllersWithViews();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.MapRazorPages();
app.UseAuthorization();
app.Use((context, next) =>
{
    Thread.CurrentPrincipal = context.User;
    return next(context);
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=MainPage}/{id?}");

app.Run();
