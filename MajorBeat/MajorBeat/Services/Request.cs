using MajorBeat.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MajorBeat.Services
{
    public  class Request
    {
        public async Task<TResult> PostAsync<TResult>(string uri, TResult data)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await httpClient.PostAsync(uri, content);
                    string serialized = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        // Retorna o objeto completo que a API devolveu
                        TResult result = JsonConvert.DeserializeObject<TResult>(serialized);
                        return result;
                    }
                    else
                    {
                        throw new Exception($"Erro ao enviar requisição POST. Status: {response.StatusCode}\nDetalhes: {serialized}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro na requisição POST: {ex.Message}");
            }
        }
    }
}
