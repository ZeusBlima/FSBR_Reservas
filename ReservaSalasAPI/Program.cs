using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using ReservaSalasLibrary.Data;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using ReservaSalasLibrary.Data.UnitOfWork;
using ReservaSalasLibrary.Data.Repositories;
using Microsoft.Extensions.Configuration;
namespace ReservaSalasAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers().PartManager.ApplicationParts.Add(new AssemblyPart(typeof(ReservaSalasLibrary.Controllers.ReservasController).Assembly));

            // Configure database connection
            builder.Services.AddDbContext<ReservaSalasContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.Configure<IWebHostEnvironment>(env => env.WebRootPath = "wwwroot");

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Configuration.SetBasePath(Path.Combine(AppContext.BaseDirectory, "../../.."));
            builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.MapControllers();
          
            app.Run();
        }
    }
}