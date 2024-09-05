using Blazored.LocalStorage;
using Client.Pages;
using SHARED.Contracts;
using SHARED.DTOs;
using SHARED.GenericModels;
using static SHARED.DTOs.ServiceResponses;

namespace Client.Services
{
    public class AccountService(HttpClient httpClient, ILocalStorageService localStorageService) : IUserAccount, IWeather
    {
        private const string BaseUrl = "api/Account";

        public async Task<GeneralResponse> CreateAccount(UserDTO userDTO)
        {
            var response = await httpClient.PostAsync($"{BaseUrl}/register", Generics.GenerateStringContent(Generics.SerializeObj(userDTO)));

            if (!response.IsSuccessStatusCode)
                return new GeneralResponse(false, "Error occured. Try again later...");

            var apiResponse = await response.Content.ReadAsStringAsync();
            return Generics.DeserializeJsonString<GeneralResponse>(apiResponse);
        }

        public async Task<LoginResponse> LoginAccount(LoginDTO loginDTO)
        {
            var response = await httpClient.PostAsync($"{BaseUrl}/login", Generics.GenerateStringContent(Generics.SerializeObj(loginDTO)));

            if (!response.IsSuccessStatusCode)
                return new LoginResponse(false, null!, "Error occured. Try again later...");

            var apiResponse = await response.Content.ReadAsStringAsync();
            var result = Generics.DeserializeJsonString<LoginResponse>(apiResponse);

            // Store the token in local storage
            await localStorageService.SetItemAsync("token", result.Token);

            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", result.Token);
            return result;
        }

        public async Task<WeatherForecast[]> GetWeatherForecast()
        {
            var token = await localStorageService.GetItemAsync<string>("token");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await httpClient.GetAsync("api/WeatherForecast/user");

            var authHeader = httpClient.DefaultRequestHeaders.Authorization?.ToString();
            Console.WriteLine($"Authorization Header: {authHeader}");

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Request failed with status code: {response.StatusCode}");
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error content: {errorContent}");
                return null;
            }

            var result = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Response content: {result}");

            return [.. Generics.DeserializeJsonStringList<WeatherForecast>(result)];
        }

        public async Task<WeatherForecast[]> GetWeatherForecastByAdmin()
        {
            var token = await localStorageService.GetItemAsync<string>("token");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await httpClient.GetAsync("api/WeatherForecast/admin");

            var authHeader = httpClient.DefaultRequestHeaders.Authorization?.ToString();
            Console.WriteLine($"Authorization Header: {authHeader}");

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Request failed with status code: {response.StatusCode}");
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error content: {errorContent}");
                return null;
            }

            var result = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Response content: {result}");

            return [.. Generics.DeserializeJsonStringList<WeatherForecast>(result)];
        }
    }
}
