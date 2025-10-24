using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using MajorBeat.Models;

namespace MajorBeat.Services;
public class MediaService
{
    private readonly HttpClient _httpClient;

    public MediaService()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://majorbeat-fzedc4ekbuaufncw.brazilsouth-01.azurewebsites.net/")
        };
        _httpClient.Timeout = TimeSpan.FromMinutes(5);
_httpClient.DefaultRequestHeaders.ExpectContinue = false;
    }

    /**
     * NOVO MÉTODO:
     * Chama o endpoint "burro" que SÓ faz o upload e retorna a URL.
     */
    
            // -----------------------------------
        

        // Este é o único método de upload que você precisa
        public async Task<List<string>> UploadVariosArquivosAsync(IEnumerable<MediaFile> mediaFiles, string token, string urlComplementar)
    {
        // Adiciona o token (ou não - vamos deixar por enquanto)
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        // 1. Usa MultipartFormDataContent (correto)
        using var content = new MultipartFormDataContent();

        // 2. USA O 'BYTEARRAYCONTENT' (O MÉTODO SEGURO)
        foreach (var mediaFile in mediaFiles)
        {
            // Abre o stream (o método seguro para Android)
            await using var stream = await mediaFile.OriginalFile.OpenReadAsync();

            // Lê o stream para a memória
            using var ms = new MemoryStream();
            await stream.CopyToAsync(ms);
            var fileBytes = ms.ToArray(); // Converte em byte[]

            // Usa o ByteArrayContent (que sabemos que o servidor aceita)
            var bytesContent = new ByteArrayContent(fileBytes);
            bytesContent.Headers.ContentType =
                new MediaTypeHeaderValue(mediaFile.OriginalFile.ContentType ?? "application/octet-stream");

            // 3. NOME "FILES" (plural - bate com a API Java)
            content.Add(bytesContent, "files", mediaFile.OriginalFile.FileName);
        }

        // 4. ENVIA A REQUISIÇÃO (que agora é idêntica à do Postman)
        var response = await _httpClient.PostAsync(urlComplementar, content);

        if (!response.IsSuccessStatusCode)
        {
            // Se ainda der 403, o problema é 100% um token inválido.
            // Se der 400/500, o problema é outro.
            var erro = await response.Content.ReadAsStringAsync();
            throw new Exception($"Falha no upload (HTTP {response.StatusCode}): {erro}");
        }

        // 5. LÊ A RESPOSTA (correto)
        var jsonResponse = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(jsonResponse);

        if (doc.RootElement.TryGetProperty("urls", out var urlsElement))
        {
            return urlsElement.Deserialize<List<string>>();
        }
        else
        {
            throw new Exception("A resposta da API não contém a propriedade 'urls'.");
        }
    }
}



