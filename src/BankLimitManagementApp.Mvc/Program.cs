using BankLimitManagementApp.Domain.Repositories;
using BankLimitManagementApp.Domain.Services;
using BankLimitManagementApp.Infra.Persistense;
using BankLimitManagementApp.Infra.Persistense.Repositories;
using BankLimitManagementApp.Mvc.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
                
builder.Services.AddScoped<IBankAccountRepository, BankAccountRepository>();
builder.Services.AddScoped<ITransactionAccountRepository, TransactionAccountRepository>();
builder.Services.AddScoped<ITransactionService, TransactionService>();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("BankConnection")));

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
