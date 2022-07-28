using ClassHelpers.Data;
using ClassHelpers.Hubs;
using ClassHelpers.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ClassHelpersContextConnection") ?? throw new InvalidOperationException("Connection string 'ClassHelpersContextConnection' not found.");

builder.Services.AddDbContext<ClassHelpersContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ClassHelpersContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication()

    .AddFacebook(options =>
    { options.AppId = "382917260579023";
      options.AppSecret = "aa56007be5587b46fe2699af9b0049ce";
}) 
    .AddGoogle(options =>
    {
        options.ClientId = "805479506263-2sfkp8tj2tqlmro6pohoruqg8uv86fo8.apps.googleusercontent.com";
        options.ClientSecret = "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";
    });

builder.Services.AddSignalR(hubOptions =>
{
    hubOptions.MaximumReceiveMessageSize = 1500000;
});
builder.Services.AddScoped<IAccountRepository, AccountRepository>();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.MapHub<ChatHub>("/chathub", options =>
{
    options.ApplicationMaxBufferSize = 1500000;
    options.TransportMaxBufferSize = 1500000;
});

app.Run();
