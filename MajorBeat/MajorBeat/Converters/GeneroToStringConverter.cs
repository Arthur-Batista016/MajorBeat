using MajorBeat.Enums;
using System;
using System.Globalization;
using Microsoft.Maui.Controls; // Certifique-se de que IValueConverter está aqui

namespace MajorBeat.Converters
{
    public class GeneroToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is NomeGenero genero)
            {
                // 1. Obtém a string do enum (ex: "BOSSA_NOVA")
                string rawName = genero.ToString();

                // 2. Substitui o underscore por espaço (ex: "BOSSA NOVA")
                string friendlyName = rawName.Replace("_", " ");

                // 3. Converte para minúsculas e depois aplica o formato de Título de Caso
                // (ex: "bossa nova" -> "Bossa Nova")
                return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(friendlyName.ToLower());
            }
            // Retorna uma string vazia se o valor não for do tipo NomeGenero
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Este método geralmente não é usado para exibição e pode permanecer assim
            throw new NotImplementedException();
        }
    }
}