namespace MAUI_calculator;

public class StringValue : Attribute
{
	public string Value;

	public StringValue(string value) { this.Value = value; }
	public string GetValue() { return Value; }
}
public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddSingleton<MainPage>();

		return builder.Build();
	}
}
