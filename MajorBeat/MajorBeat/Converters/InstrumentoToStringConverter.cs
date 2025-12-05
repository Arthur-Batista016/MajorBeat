using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace MajorBeat.Converters
{
    public class InstrumentoToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Enum instrumento)
            {
                // 1. Obtém a string do nome do Enum (Ex: "BATERIA")
                string enumName = instrumento.ToString();

                // 2. Converte para minúsculo (Ex: "bateria")
                string lowerCaseName = enumName.ToLower();

                // 3. Converte o primeiro caractere para maiúsculo (Ex: "Bateria")
                // Certifique-se de que a string não está vazia.
                if (lowerCaseName.Length > 0)
                {
                    return char.ToUpper(lowerCaseName[0]) + lowerCaseName.Substring(1);
                }
                return string.Empty;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}