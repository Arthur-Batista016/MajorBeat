using MajorBeat.Enums;
using MajorBeat.Models;
using MajorBeat.ViewModels.Hirer;
using MajorBeat.Views.Musicians;
using System.Collections.ObjectModel;
using System.Diagnostics.Tracing;

namespace MajorBeat.Views.Hirers;

public partial class HirerCalendarPage : ContentPage
{
    public HirerCalendarPage()
    {
        InitializeComponent();
        BindingContext = new HirerCalendarViewModel();


    }
    private async void create_event_btn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CreateEventPageView());//CreateEventPageView()
    }
    private async void search_page_btn_Clicked(object sender, EventArgs e) =>
        await Navigation.PushAsync(new HirerSearchPage());

    private async void home_page_btn_Clicked(object sender, EventArgs e) =>
        await Navigation.PushAsync(new HirerHomePage());

    private async void profile_page_btn_Clicked(object sender, EventArgs e) =>
        await Navigation.PushAsync(new HirerProfilePage());

}
