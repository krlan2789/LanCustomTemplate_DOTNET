using CustomTemplate.MAUI.Resources;

namespace CustomTemplate.MAUI;

partial class App : Application
{
	private readonly AppShell appShell;

	public App(AppShell shell)
	{
		Resources = new AppStyles();
		appShell = shell;
	}

	protected override Window CreateWindow(IActivationState? activationState) => new(appShell);
}