using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PruebasTechSLucia.Services;

public class Program
{
	public static void Main(string[] args)
	{
		CreateHostBuilder(args).Build().Run();
	}

	public static IHostBuilder CreateHostBuilder(string[] args) =>
		Host.CreateDefaultBuilder(args)
			.ConfigureWebHostDefaults(webBuilder =>
			{
				webBuilder.ConfigureServices((hostContext, services) =>
				{

					services.AddSingleton<ISubcription, Subcription>();
					services.AddSingleton<IAuthorization, Authorization>();
					services.AddSingleton<ITransaction, Transaction>();
					services.AddControllersWithViews();
					services.AddSession();
				})
				.Configure(app =>
				{
					
					app.UseExceptionHandler("/Home/Error");
						// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
					app.UseHsts();

					// Configura otros middlewares aquí

					app.UseSession();
					app.UseHttpsRedirection();
					app.UseStaticFiles();

					app.UseRouting();

					app.UseAuthorization();
					
					app.UseEndpoints(endpoints =>
					{
						endpoints.MapControllerRoute(
							name: "default",
							pattern: "{controller=Home}/{action=Index}/{id?}");
					});
				});
			});
}