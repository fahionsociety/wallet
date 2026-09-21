using Microsoft.EntityFrameworkCore;
using Wallet.Api.Data;
using Wallet.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<WalletDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHttpClient<WalletWalletService>();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();