using CommunityToolkit.Maui;

namespace CustomTemplate.MAUI;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder()
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.UseMauiCommunityToolkitMarkup()
			.UseMauiCommunityToolkitCamera()
			.UseMauiCommunityToolkitMediaElement()
			.ConfigureFonts(fonts => fonts.AddFont("FontAwesome.otf", "FontAwesome"))
			.ConfigureMauiHandlers(handlers =>
		{
#if IOS || MACCATALYST
			handlers.AddHandler<CollectionView, Microsoft.Maui.Controls.Handlers.Items2.CollectionViewHandler2>();
			handlers.AddHandler<CarouselView, Microsoft.Maui.Controls.Handlers.Items2.CarouselViewHandler2>();
#endif
		});
		// App Shell
		builder.Services.AddTransient<AppShell>();
		// Services
		builder.Services.AddSingleton<App>();
		builder.Services.AddSingleton(Browser.Default);
		builder.Services.AddSingleton(Preferences.Default);
		// Pages + View Models
		// builder.Services.AddTransient<NewsPage, NewsViewModel>();
		// builder.Services.AddTransient<SettingsPage, SettingsViewModel>();
		// builder.Services.AddTransient<NewsDetailPage, NewsDetailViewModel>();
		// C# Hot Reload Handler
		builder.Services.AddSingleton<ICommunityToolkitHotReloadHandler, HotReloadHandler>();
		return builder.Build();
	}
}