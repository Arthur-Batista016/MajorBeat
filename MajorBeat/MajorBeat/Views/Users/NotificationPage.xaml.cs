using MajorBeat.Models;
using MajorBeat.ViewModels.Users;
using MajorBeat.Views.Users;
using Syncfusion.Maui.Core.Carousel;

namespace MajorBeat.Views.Hirers;

public partial class NotificationPage : ContentPage
{


    private readonly ProposalViewModel _viewModel;
    public NotificationPage()
    {
        InitializeComponent();
        BindingContext = new ProposalViewModel();
    }
    
    private async void voltar_Clicked_1(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new HirerHomePage());
    }


    //TAP GESTURE 

    private async void OnNotifyTapped(object sender, EventArgs e)
    {


        if (sender is VisualElement element && element.BindingContext is Proposta proposta)
        {
            // Executa o comando (se quiser manter a lógica dentro da ViewModel)
            _viewModel.ClickProposalCommand.Execute(proposta);

            // Navegação async passando o ID
            await Navigation.PushAsync(new ProposalPageView(proposta.Id));
        }


    }


}