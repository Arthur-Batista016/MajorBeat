using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MajorBeat.Converters
{
    public class GeneroWidthConverter:IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // value é o nome do gênero
            if (value is string genero)
            {
                if (genero.ToUpper() == "BOSSA_NOVA")
                    return 180; // largura maior para Bossa Nova
                else
                    return 100; // largura padrão para outros
            }
            return 125;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
