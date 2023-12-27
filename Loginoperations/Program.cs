using Loginoperations.Context;
using Loginoperations.Dto;
using Loginoperations.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddDbContext<ProductContext>(x=>x.UseSqlite("Data Source=products.db"));
/*builder.Services.AddDbContext<UserContext>(x => x.UseSqlite(("Data Source=login.db")));
builder.Services.AddDbContext<ProductContext>(x=>x.UseSqlite("Data Source=login.db"));
builder.Services.AddDbContext<CartContext>(x=>x.UseSqlite("Data Source=login.db"));*/
//builder.Services.AddDbContext<DContext>(x => x.UseSqlite(("Data Source=project.db")));
//builder.Services.AddDbContext<DContext>(x => x.UseSqlServer("Data Source=SQL9001.site4now.net;Initial Catalog=db_aa1683_sales;User Id=db_aa1683_sales_admin;Password=Ef123456789"));
builder.Services.AddDbContext<DContext>(x=>x.UseSqlServer("Data Source=SQL9001.site4now.net;Initial Catalog=db_aa2f9f_sales;User Id=db_aa2f9f_sales_admin;Password=Ef123456789"));
builder.Services.AddScoped<PasswordService>();
builder.Services.AddScoped<MailService>();
builder.Services.AddScoped<DtoConverter>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin",
        builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();