using MajorBeat.Services.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MajorBeat.ViewModels
{
    public class PrincipalViewModel : BaseViewModel
    {
        private readonly UsuarioService _userService;
        public MusicianCreateAccountViewModel MusicoVM { get; }
        public HirerCreateAccountViewModel ContratanteVM { get; }
        private readonly INavigation _navigation;


        private bool isMusicoSelected;
        public bool IsMusicoSelected
        {
            get => isMusicoSelected;
            set
            {
                if (isMusicoSelected != value)
                {
                    isMusicoSelected = value;
                    onPropertyChanged(nameof(IsMusicoSelected));       // avisa que mudou
                    onPropertyChanged(nameof(IsContratanteSelected));
                }
            }
        }

        public bool IsContratanteSelected => !isMusicoSelected;

        private bool isEmpresaSelected;
        public bool IsEmpresaSelected
        {
            get => isEmpresaSelected;
            set
            {
                if (isEmpresaSelected != value)
                {
                    isEmpresaSelected = value;
                    onPropertyChanged(nameof(IsEmpresaSelected));       // avisa que mudou
                    onPropertyChanged(nameof(IsContraSelected));
                }
            }
        }

        public bool IsContraSelected => !isEmpresaSelected;

        public ICommand ConfirmarCommand { get; }

        public PrincipalViewModel(INavigation navigation)
        {
            _navigation = navigation;
            
            MusicoVM = new MusicianCreateAccountViewModel(_navigation);//_navigation
            ContratanteVM = new HirerCreateAccountViewModel(_navigation);


            ConfirmarCommand = new Command(async () => await OnConfirmarClicked());
        }

        private async Task OnConfirmarClicked()
        {
            if (IsMusicoSelected)
            {
                //await _navigation.PushAsync(new MusicianProfileView());
                await MusicoVM.UserSave();
            }
            else
            {
                await ContratanteVM.UserSave();
            }
        }
    }
}
