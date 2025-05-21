using Monaem.Response;
using System.Net.Http.Json;

namespace Monaem.APIHandlerService
{
    public class APIHandlerService
    {
        private readonly HttpClient _httpClient;

        public APIHandlerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ReciveResponse<T>> getData<T>(string apiEndpoint)
        {
            ReciveResponse<T> Reciveresponse = new ReciveResponse<T>();

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, apiEndpoint);

                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        Reciveresponse = await response.Content.ReadFromJsonAsync<ReciveResponse<T>>();
                    }
                    catch (Exception ex)
                    {
                        Reciveresponse.ErrorMessageg = ex.Message;
                    }
                }
                else
                {
                    Reciveresponse.ErrorMessageg = await response.Content.ReadAsStringAsync();
                }
            }catch (Exception ex)
            {
                Reciveresponse.ErrorMessageg = ex.Message;
            }

            return Reciveresponse;
        }
        public async Task<ReciveResponse<T>> PostData<T>(object data, string apiEndpoint)
        {
            ReciveResponse<T> Reciveresponse = new ReciveResponse<T>();
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, apiEndpoint)
                {
                    Content = JsonContent.Create(data) 
                };

                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        Reciveresponse = await response.Content.ReadFromJsonAsync<ReciveResponse<T>>();
                    }
                    catch (Exception ex)
                    {
                        Reciveresponse.ErrorMessageg = ex.Message;
                    }
                }
                else
                {
                    Reciveresponse.ErrorMessageg =  await response.Content.ReadAsStringAsync();
                }
            }
            catch(Exception ex)
            {
                Reciveresponse.ErrorMessageg = ex.Message;
            }
            return Reciveresponse;
        }
    }
}

