using Bookshelf.DataAccess.Repository;
using Bookshelf.DataAccess.Repository.IRepositury;
using BookshelfWeb.DataAccess.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder
    .Services.AddDefaultIdentity<IdentityUser>()
    .AddEntityFrameworkStores<ApplicationDBContext>();

// Add services to the razor pages.
builder.Services.AddRazorPages();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

#region localIP
// // Setup local IP\
// static string LocalIPAddress()
// {
//     using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
//     {
//         socket.Connect("8.8.8.8", 65530);
//         if (socket.LocalEndPoint is IPEndPoint endPoint)
//         {
//             return endPoint.Address.ToString();
//         }
//         else
//         {
//             return "127.0.0.1";
//         }
//     }
// }
// string localIP = LocalIPAddress();

// app.Urls.Add("http://" + localIP + ":5160");
// app.Urls.Add("https://" + localIP + ":7022");
#endregion

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
app.UseAuthorization();

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}"
);

app.Run();
