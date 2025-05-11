namespace CustomTemplate.MAUI;

partial class AppShell : Shell
{
	private static readonly ReadOnlyDictionary<Type, string> pageRouteMappingDictionary = new Dictionary<Type, string>([]).AsReadOnly();

	public AppShell()
	{
	}

	public static string GetRoute(Type type)
	{
		if (!pageRouteMappingDictionary.TryGetValue(type, out var route))
		{
			throw new KeyNotFoundException($"No map for ${type} was found on navigation mappings. Please register your ViewModel in {nameof(AppShell)}.{nameof(pageRouteMappingDictionary)}");
		}

		return route;
	}
}