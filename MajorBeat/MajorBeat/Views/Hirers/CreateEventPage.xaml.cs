namespace MajorBeat.Views.Hirers
{
    public partial class CreateEventPage : ContentPage
    {
        public CreateEventPage()
        {
            InitializeComponent();
            EventDatePicker.MinimumDate = DateTime.Today;
        }

        private async void OnCategoryChosen(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            if (picker.SelectedIndex < 0) return;

            // pega o texto do item (quando usamos Items)
            var selected = picker.Items[picker.SelectedIndex];

            await DisplayAlert("Categoria", $"Você escolheu: {selected}", "OK");
        }
    }
}