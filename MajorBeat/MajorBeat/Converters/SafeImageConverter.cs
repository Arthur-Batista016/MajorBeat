using System;
using System.Globalization;
using Microsoft.Maui.Controls; // Certifique-se de que isso está sendo usado
using Microsoft.Maui.Graphics;

namespace MajorBeat.Converters
{
    public class SafeImageConverter : IValueConverter
    {
        // Nome do seu arquivo de imagem local.
        private const string PlaceholderImage = "placeholder.png";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var url = value as string;

            // 1. Caso de URL Nula/Vazia
            if (string.IsNullOrWhiteSpace(url))
            {
                // Retorna um ImageSource criado a partir de um arquivo local
                return ImageSource.FromFile(PlaceholderImage);
            }

            try
            {
                // 2. Caso de URL Válida
                // Tenta criar um ImageSource a partir da URI (URL)
                return ImageSource.FromUri(new Uri(url));
            }
            catch (Exception ex)
            {
                // Opcional: Você pode logar o erro "ex" aqui para debug.
                System.Diagnostics.Debug.WriteLine($"Erro ao carregar URL: {url}. Exceção: {ex.Message}");

                // 3. Caso de Falha (URL inválida ou erro de URI)
                // Retorna a imagem padrão como ImageSource
                return ImageSource.FromFile(PlaceholderImage);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}