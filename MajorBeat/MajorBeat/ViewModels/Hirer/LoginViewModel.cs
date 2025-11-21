using MajorBeat.ModelsJeff;
using MajorBeat.Services.Usuarios;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Input;

namespace MajorBeat.ViewModels.Hirer;

public class LoginViewModel : BaseViewModel
{
    private bool IsLoading = false;
    public ICommand AutenticarCommand { get; set; }
    private UsuarioService _uService;
    public LoginViewModel()
    {
        _uService = new UsuarioService();
        AutenticarCommand = new Command(async () => await AutenticarUsuario());
    }
    public async Task AutenticarUsuario()
    {
        if (IsLoading)
        { return;
        }
        IsLoading = true;

        try
        {
            try
            {
                Contratante c = new Contratante();

                c.nome = Nome;
                c.email = Email;
                c.senha = Senha;
                var service = new UsuarioService();
                Contratante ca = await service.PostAutenticarUsuarioAsync(c);

                Preferences.Set("UsuarioToken", ca.token);
                Preferences.Set("Usuarioid", ca.idContratante);

                await Application.Current.MainPage.DisplayAlert(
                    "Sucesso",
                    $"Contratante {c.nome} autenticado com sucesso!",
                    "OK"
                );
                await Application.Current.MainPage.Navigation.PushAsync(new Views.Hirers.HirerHomePage());
            }
            catch
            {
                Musico m = new Musico();
                m.nome = Nome;
                m.email = Email;
                m.senha = Senha;
                var service = new UsuarioService();
                Musico ma = await service.PostAutenticarUsuarioMAsync(m);
                Preferences.Set("UsuarioToken", ma.token);
                Preferences.Set("Usuarioid", ma.idMusico);
                await Application.Current.MainPage.DisplayAlert(
                    "Sucesso",
                    $"Musico {m.nome}autenticado com sucesso!",
                    "OK"
                );
                await Application.Current.MainPage.Navigation.PushAsync(new Views.Musicians.MusicianHomePage());
            }


        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert(
                    "Erro ao cadastrar",
                    $"Não foi possível concluir o cadastro.\nDetalhes: {ex.Message}",
                    "OK"
                );


        }
        finally
        {
            IsLoading = false;
        }

    }
    private string nome = string.Empty;
    public string Nome
    {
        get { return nome; }
        set
        {
            nome = value;
            onPropertyChanged();
        }
    }

    private string email = string.Empty;
    public string Email
    {
        get { return email; }
        set
        {
            email = value;
            onPropertyChanged();
        }
    }

    private string senha = string.Empty;
    public string Senha
    {
        get { return senha; }
        set
        {
            senha = value;
            onPropertyChanged();
        }

    }
}