namespace CustomTemplate.MAUI.Resources;

public partial class AppStyles : ResourceDictionary
{
    public static Color PrimaryColorLight { get; } = Color.FromArgb("#79678F");
    public static Color PrimaryColorDark { get; } = Color.FromArgb("#FFDD00");

    public static Color SecondaryColorLight { get; } = Color.FromArgb("#334155");
    public static Color SecondaryColorDark { get; } = Color.FromArgb("#FFFCE6");

    public static Color TertiaryColorLight { get; } = Color.FromArgb("#FFFFFF");
    public static Color TertiaryColorDark { get; } = Color.FromArgb("#252525");

    public static Color QuaternaryColorLight { get; } = Color.FromArgb("#F0F3FF");
    public static Color QuaternaryColorDark { get; } = Color.FromArgb("#404040");

    public static Color DarkColor { get; } = Color.FromArgb("#000000");
    public static Color LightColor { get; } = Color.FromArgb("#C8CCCE");
    public static Color DangerColor { get; } = Color.FromArgb("#A52A2A");
    public static Color TransparentColor { get; } = Color.FromArgb("#FFFFFF00");

    private static readonly Color browserNavigationBarTextColorDark = LightColor;
    private static readonly Color browserNavigationBarTextColorLight = DarkColor;
    private static readonly Color browserNavigationBarBackgroundColorDark = TertiaryColorLight;
    private static readonly Color browserNavigationBarBackgroundColorLight = TertiaryColorDark;
    private static readonly Color pageBackgroundColorLight = QuaternaryColorLight;
    private static readonly Color pageBackgroundColorDark = QuaternaryColorDark;

    public static Color PreferredControlColor { get; } = App.Current?.RequestedTheme is AppTheme.Dark ? browserNavigationBarTextColorDark : browserNavigationBarTextColorLight;

    public static Color PreferredToolbarColor { get; } = App.Current?.RequestedTheme is AppTheme.Dark ? browserNavigationBarBackgroundColorDark : browserNavigationBarBackgroundColorLight;

    public static Style PrimaryButtonStyle { get; } = new Style<Button>()
        .AddAppThemeBinding(Button.TextColorProperty, DarkColor, LightColor)
        .AddAppThemeBinding(Button.BackgroundColorProperty, PrimaryColorLight, PrimaryColorDark);

    public static Style LabelStyle { get; } = new Style<Label>()
        .AddAppThemeBinding(Label.TextColorProperty, DarkColor, LightColor)
        .AddAppThemeBinding(Label.BackgroundColorProperty, TransparentColor, TransparentColor);

    public static Style ValidEntryNumericValidationStyle { get; } = new Style<Entry>()
        .AddAppThemeBinding(Entry.TextColorProperty, pageBackgroundColorLight, pageBackgroundColorDark);

    public static Style InvalidEntryNumericValidationStyle { get; } = new Style<Entry>()
        .AddAppThemeBinding(Entry.TextColorProperty, DangerColor, DangerColor);

    public static Style NavigationPageStyle { get; } = new Style<NavigationPage>()
        .AddAppThemeBinding(NavigationPage.BarTextColorProperty, DarkColor, LightColor)
        .AddAppThemeBinding(NavigationPage.BackgroundColorProperty, pageBackgroundColorLight, pageBackgroundColorDark)
        .AddAppThemeBinding(NavigationPage.BarBackgroundColorProperty, PrimaryColorLight, PrimaryColorDark)
        .ApplyToDerivedTypes(true);

    public static Style ShellStyle { get; } = new Style<Shell>()
        .AddAppThemeBinding(Shell.NavBarHasShadowProperty, true, true)
        .AddAppThemeBinding(Shell.TitleColorProperty, DarkColor, LightColor)
        .AddAppThemeBinding(Shell.DisabledColorProperty, DarkColor, LightColor)
        .AddAppThemeBinding(Shell.UnselectedColorProperty, DarkColor, LightColor)
        .AddAppThemeBinding(Shell.ForegroundColorProperty, DarkColor, LightColor)
        .AddAppThemeBinding(Shell.BackgroundColorProperty, PrimaryColorLight, PrimaryColorDark).ApplyToDerivedTypes(true);

    public AppStyles()
    {
        // all the colors and styles are being accessed directly except the below two
        Add(NavigationPageStyle);
        Add(ShellStyle);
    }
}