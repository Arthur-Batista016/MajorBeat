using MajorBeat.Models;
using MajorBeat.Services.Usuarios;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Input;

namespace MajorBeat.ViewModels.Hirer;

public class LoginViewModel : BaseViewModel
{
    public ICommand AutenticarCommand { get; set; }
    private UsuarioService _uService;
    public LoginViewModel()
    {
        _uService = new UsuarioService();
        AutenticarCommand = new Command(async () => await AutenticarUsuario());
    }
    public async Task AutenticarUsuario()
    {
        try
        {
            Contratante c = new Contratante();

            c.email = Email;
            c.senha = Senha;
            Contratante contratanteAutenticado = await _uService.PostAutenticarUsuarioAsync(c);

            Preferences.Set("UsuarioToken", contratanteAutenticado.token);

            await Application.Current.MainPage.DisplayAlert(
                "Sucesso",
                $"Contratante {contratanteAutenticado.nome} cadastrado com sucesso!\nID: {contratanteAutenticado.id}",
                "OK"
            );
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert(
                    "Erro ao cadastrar",
                    $"Não foi possível concluir o cadastro.\nDetalhes: {ex.Message}",
                    "OK"
                );


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