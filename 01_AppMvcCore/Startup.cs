using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using VitalTools.Database.Formation;
using _01_AppMvcCore.Services;

namespace _01_AppMvcCore
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }



		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services/*, IWebHostEnvironment env*/) // Ajout de 'IWebHostEnvironment' pour test de l'environnement de travail.
		{
			#region Injection de d�pendance du Service d'authentification
			
			services.AddSingleton<IAuthService, AuthService>();

			#endregion

			#region 01 - Configuration d'une Session

			// Ajout d'un espace en m�moire pour stocker les entit�s
			services.AddDistributedMemoryCache();

			// Cr�ation et configuration de la Session
			services.AddSession(options =>
			{
				// On indique combien de temps la session est maintenue si l'utilisateur est inactif.
				options.IdleTimeout = TimeSpan.FromMinutes(20);
				// On indique si le cookie de session sera disponible dans les scripts cot�-client.
				options.Cookie.HttpOnly = true;
				// On indique si le cookie est indispensable au fonctionnement du site.
				options.Cookie.IsEssential = true;
			});

			#endregion

			#region Secure your Cookies

			// Hello C'est Romain ! (test)
			services.Configure<CookiePolicyOptions>(options =>
			{
				options.MinimumSameSitePolicy = SameSiteMode.Strict;
				options.HttpOnly = HttpOnlyPolicy.Always;
				options.Secure = CookieSecurePolicy.Always;
				//options.Secure = env.IsDevelopment() ? CookieSecurePolicy.None : CookieSecurePolicy.Always;
			});

			#endregion

			services.AddControllersWithViews();
		}





		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();



			#region 02 - On indique � l'application d'utiliser un syst�me de 'Session'

			app.UseSession();

			#endregion



			#region Secure Your Cookies

			app.UseCookiePolicy();

			#endregion



			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
