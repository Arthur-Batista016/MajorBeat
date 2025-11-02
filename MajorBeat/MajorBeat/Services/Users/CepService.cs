namespace MajorBeat.Services.Users;

using MajorBeat.Models;

// Nome do arquivo: CepService.cs
using System.Text.Json;
using System.Text.RegularExpressions;

public class CepService
{
    // É uma boa prática usar um HttpClient estático ou injetado
    // (via IHttpClientFactory) para evitar exaustão de sockets.
    private static readonly HttpClient _httpClient = new HttpClient();

    /// <summary>
    /// Busca um CEP e retorna o endereço formatado em uma única string.
    /// Retorna 'null' se o CEP for inválido ou não encontrado.
    /// </summary>
    /// <param name="cep">O CEP para consultar (pode conter máscara)</param>
    /// <returns>Uma string com o endereço formatado ou null.</returns>
    public async Task<string> BuscarEnderecoFormatadoAsync(string cep)
    {
        // 1. Limpar e validar o formato do CEP
        // Remove tudo que não for dígito
        var cepLimpo = Regex.Replace(cep ?? "", @"[^\d]", "");

        if (cepLimpo.Length != 8)
        {
            // Formato inválido
            return null;
        }

        string url = $"https://viacep.com.br/ws/{cepLimpo}/json/";

        try
        {
            // 2. Fazer a requisição HTTP
            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                // 3. Ler o stream da resposta e desserializar o JSON
                await using var contentStream = await response.Content.ReadAsStreamAsync();
                var endereco = await JsonSerializer.DeserializeAsync<ViaCepResponse>(contentStream);

                // 4. Validar se a API encontrou o CEP
                // O ViaCEP retorna "erro: true" se o CEP não existe
                if (endereco == null || endereco.Erro)
                {
                    // CEP não encontrado na base do ViaCEP
                    return null;
                }

                // 5. Formatar a string de saída como solicitado
                return $"{endereco.Logradouro}, {endereco.Bairro} - {endereco.Localidade}/{endereco.Uf}";
            }
            else
            {
                // A requisição HTTP falhou (ex: 404, 500)
                return null;
            }
        }
        catch (Exception ex)
        {
            // Erro de rede, JSON inválido, etc.
            // (Você pode querer logar esse erro)
            Console.WriteLine($"Erro ao consultar CEP: {ex.Message}");
            return null;
        }
    }
}