using System.Text.Json;

namespace Deliveroo.Services;

public class UnsplashService
{
	private readonly HttpClient _http;
	private readonly string _accessKey;

	public UnsplashService(HttpClient http, IConfiguration config)
	{
		_http = http;
		_accessKey = config["Unsplash:AccessKey"];
	}

	// Cerca foto per keyword (es. "pizza", "sushi", "burger")
	public async Task<string?> GetPhotoUrlAsync(string query)
	{
		var url = $"https://api.unsplash.com/search/photos?query={Uri.EscapeDataString(query)}&per_page=1&client_id={_accessKey}";

		var response = await _http.GetAsync(url);
		if (!response.IsSuccessStatusCode) return null;

		var json = await response.Content.ReadAsStringAsync();
		using var doc = JsonDocument.Parse(json);

		var results = doc.RootElement.GetProperty("results");
		if (results.GetArrayLength() == 0) return null;

		// Ritorna l'URL della foto in formato "regular" (800px circa)
		return results[0]
			.GetProperty("urls")
			.GetProperty("regular")
			.GetString();
	}
}