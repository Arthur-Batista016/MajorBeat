using System;
using System.Collections.ObjectModel;
using System.Globalization;
using MajorBeat.Models;
using Microsoft.Maui.Controls;

// *ASSUMA que a classe Avaliacao está definida em MajorBeat.Models*
// public class Avaliacao { public double nota { get; set; } }

namespace MajorBeat.Converters
{
    public class notaConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // 1. Tenta converter o valor para a coleção esperada
            if (value is ObservableCollection<Avaliacao> avaliacoes && avaliacoes.Count > 0)
            {
                // 2. Se a lista NÃO é nula e NÃO é vazia, acessa o primeiro item
                var primeiraAvaliacao = avaliacoes[0];

                // 3. Verifica se a Avaliacao em si não é nula
                if (primeiraAvaliacao != null)
                {
                    // 4. Verifica se a propriedade 'nota' é 0
                    if (primeiraAvaliacao.nota == 0 || primeiraAvaliacao == null)
                    {
                        return "Sem avaliação";
                    }

                    // 5. Se for maior que zero, retorna o valor da nota formatado
                    // O valor é convertido para string, por exemplo: 4.5 -> "4.5"
                    return primeiraAvaliacao.nota.ToString("F1", CultureInfo.InvariantCulture);
                }
            }

            // 6. Se a lista for nula, vazia, ou o primeiro item for nulo, retorna a mensagem
            return "Sem avaliação";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}