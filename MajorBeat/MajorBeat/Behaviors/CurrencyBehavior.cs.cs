using System.Globalization;
using CommunityToolkit.Maui.Behaviors; // Se estiver usando o Community Toolkit

namespace MajorBeat.Behaviors
{
    public class CurrencyBehavior : Behavior<Entry>
    {
        // Método chamado quando o Behavior é anexado ao Entry
        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.TextChanged += OnEntryTextChanged;
        }

        // Método chamado quando o Behavior é desanexado do Entry
        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(bindable);
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            var entry = (Entry)sender;
            string valorAtual = args.NewTextValue;

            // Impede que o cursor pule no início
            if (entry.CursorPosition < 0) return;

            // 1. Limpa o valor (mantém apenas dígitos)
            string valorLimpo = new string(valorAtual.Where(char.IsDigit).ToArray());

            if (string.IsNullOrWhiteSpace(valorLimpo))
            {
                // Se o campo estiver vazio, define como string vazia (ou R$ 0,00 se preferir)
                entry.Text = string.Empty;
                return;
            }

            // 2. Trata o valor como centavos e formata
            if (decimal.TryParse(valorLimpo, out decimal valorDecimal))
            {
                // Divide por 100: 12345 -> 123,45
                valorDecimal /= 100;

                // Formatação: Moeda (C) na cultura brasileira (pt-BR)
                var culturaBrasileira = new CultureInfo("pt-BR");
                string valorFormatado = string.Format(culturaBrasileira, "{0:C}", valorDecimal);

                // 3. Aplica o valor formatado ao Entry
                if (entry.Text != valorFormatado)
                {
                    // Desliga temporariamente para evitar loop
                    entry.TextChanged -= OnEntryTextChanged;
                    entry.Text = valorFormatado;
                    entry.TextChanged += OnEntryTextChanged;
                }

                // Garante que o cursor permaneça no final
                entry.CursorPosition = entry.Text.Length;
            }
        }
    }
}