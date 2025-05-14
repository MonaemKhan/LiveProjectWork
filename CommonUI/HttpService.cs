using CentralModels.Administration;
using System.Net.Http.Headers;

namespace CommonUI
{
    public class HttpService
    {
        private readonly HttpClient _httpClient;
        private readonly ISessionService _sessionService;

        public HttpService(IHttpClientFactory factory, ISessionService sessionService)
        {
            _httpClient = factory.CreateClient("ApiClient");
            _sessionService = sessionService;
        }

        public async Task<string> getLoginResponce(LoginModel loginData)
        {
            string error = "";
            var response = await _httpClient.PostAsJsonAsync("apiV1/Login", loginData);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<LoginResponce>();
                await _sessionService.setLoginResponce(result);
            }
            else
            {
                error = await response.Content.ReadAsStringAsync();
            }

            return error;
        }

        public async Task<(T, string)> getData<T>(string apiEndpoint)
        {
            var error = "";

            var request = new HttpRequestMessage(HttpMethod.Get, apiEndpoint);

            request.Headers.Add("AuthenticationKey", await _sessionService.getToken());

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return (await response.Content.ReadFromJsonAsync<T>(), "");
                }
                catch (Exception ex)
                {
                    return (default(T), ex.Message);
                }
            }
            else
            {
                error = await response.Content.ReadAsStringAsync();
                return (default(T), error);
            }
        }

        public async Task<(T, string)> PostData<T>(T data, string apiEndpoint)
        {
            var error = "";

            var request = new HttpRequestMessage(HttpMethod.Post, apiEndpoint) { Content = JsonContent.Create(data) };


            request.Headers.Add("AuthenticationKey", await _sessionService.getToken());

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return (await response.Content.ReadFromJsonAsync<T>(), "");
                }
                catch (Exception ex)
                {
                    return (default(T), ex.Message);
                }
            }
            else
            {
                error = await response.Content.ReadAsStringAsync();
                return (default(T), error);
            }
        }
    }
}
