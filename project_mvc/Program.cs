using project_mvc.Helpers;

var builder = WebApplication.CreateBuilder(args);

_ = new WebConfig(builder.Configuration, builder.Environment);

builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromHours(2);
});

var app = builder.Build();

app.UseStaticFiles(new StaticFileOptions()
{
	OnPrepareResponse = r =>
	{
		string path = r.File.PhysicalPath!;
		if (path.EndsWith(".css") || path.EndsWith(".js") || path.EndsWith(".gif") || path.EndsWith(".jpg") || path.EndsWith(".jpeg") || path.EndsWith(".png") || path.EndsWith(".svg") || path.EndsWith(".woff2") || path.EndsWith(".otf") || path.EndsWith(".ttf") || path.EndsWith(".webp"))
		{
			if (app.Environment.IsDevelopment())
			{
				r.Context.Response.Headers.Append("Cache-Control", "no-cache, no-store");
			}
			else
			{
				TimeSpan maxAge = new(365, 0, 0, 0);
				r.Context.Response.Headers.Append("Cache-Control", "max-age=" + maxAge.TotalSeconds);
			}
		}
	}
});

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseSession();
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

//app.MapStaticAssets();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();