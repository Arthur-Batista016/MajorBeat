using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MajorBeat.Services
{
    public class Request
    {
        public async Task<int> PostReturnIntAsync<TResult>(string uri, TResult data, string token)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization
            = new AuthenticationHeaderValue("Bearer", token);

            var content = new StringContent(JsonConvert.SerializeObject(data));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await httpClient.PostAsync(uri, content);

            string serialized = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return int.Parse(serialized);
            else
                throw new Exception(serialized);
        }


        public async Task<TResult> PostAsync<TResult>(string uri, TResult data, string token)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var serializerSettings = new JsonSerializerSettings
            {
                // 2. Resolve o problema da nomenclatura (PascalCase -> camelCase)
                ContractResolver = new CamelCasePropertyNamesContractResolver(),

                DateFormatString = "yyyy-MM-ddTHH:mm:ss"
            };

            // 3. Adiciona o conversor para resolver o problema dos Enums (Número -> String)
            serializerSettings.Converters.Add(new StringEnumConverter());

            // 4. Serialize o objeto usando as novas configurações
            var json = JsonConvert.SerializeObject(data, serializerSettings);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            /*exibir json post
                        // 1. Converte o objeto para uma string JSON
                        var jsonPayload = JsonConvert.SerializeObject(data);

                        // 2. EXIBE O JSON NA JANELA DE OUTPUT DO VISUAL STUDIO
                        Debug.WriteLine($"JSON ENVIADO: {jsonPayload}");

                        // 3. Usa a string JSON que acabou de criar
                        var content = new StringContent(jsonPayload);
            */


            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await httpClient.PostAsync(uri, content);
            string serialized = await response.Content.ReadAsStringAsync();
            TResult result = data;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                result = await Task.Run(() => JsonConvert.DeserializeObject<TResult>(serialized));
            else throw new Exception(serialized);

            return result;
        }

        public async Task<TResult> PutAsync<TResult>(string uri, TResult data, string token)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Adiciona o cabeçalho de 'Accept'
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PutAsync(uri, content);

            string serialized = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                // Se a resposta for OK (200) ou No Content (204)
                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    // Se o servidor retornar 204 (No Content), retorne o objeto original (data) ou o valor padrão.
                    return data;
                }

                // 🚨 Desserializa o JSON de volta para o tipo esperado (TResult)
                return JsonConvert.DeserializeObject<TResult>(serialized);
            }
            else
            {
                // Se a API retornar um erro (4xx ou 5xx), lance uma exceção detalhada
                throw new HttpRequestException($"Erro HTTP {response.StatusCode} ao executar PUT. Detalhes da API: {serialized}");
            }
        }


        public async Task<TResult> GetAsync<TResult>(string uri, string token)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await httpClient.GetAsync(uri).ConfigureAwait(false);

            // CORREÇÃO 2: Adicionar ConfigureAwait(false) na leitura do conteúdo
            string serialized = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception(serialized);

            TResult result = await Task.Run(() => JsonConvert.DeserializeObject<TResult>(serialized));
            return result;
        }

        public async Task<int> DeleteAsync(string uri, string token)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await httpClient.DeleteAsync(uri);
            string serialized = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return int.Parse(serialized);
            else
                throw new Exception(serialized);
        }

    }
}
