namespace MAUI_calculator;

//thanks to Amit Rai Sharma on Stack Overflow for this solution for adding a string to an enum (https://stackoverflow.com/questions/8588384/how-to-define-an-enum-with-string-value) 
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
				fonts.AddFont("digital-7.ttf", "Digital-7");
			});

		builder.Services.AddSingleton<MainPage>();

		return builder.Build();
	}
}
