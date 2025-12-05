using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MajorBeat.Models;
using MajorBeat.Services.Musicians;
using MajorBeat.Services.Users;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MajorBeat.ViewModels.Hirers
{
    public partial class HirerHomePageViewModel : ObservableObject
    {

        public HirerHomePageViewModel()
        {

            _mService = new MusicianService();
            currentToken = _mService.Token;
            ActualPosition = 0;
            EventPhoto = new ObservableCollection<string>();
            musicos = new ObservableCollection<Musico>();

            ExibirTodosMusicos(16);

            ClickMusicianCommand = new Command<Musico>(async (musico) => await MusicianTapped(musico));

        }



        MusicianService _mService;

        private ObservableCollection<Musico> musicos;

        [ObservableProperty]
        private string currentToken;

        [ObservableProperty]
        private string marginGenero = "100";


        

        public ICommand GerarCommand { get; set; }


        private ObservableCollection<string> eventPhoto;
        private int actualPosition;

        [ObservableProperty]
        public bool hasMusicians;

        private string namevent;
        private string title;
        private DateTime data;
        public string NameEvent
        {
            get => namevent;
            set
            {
                if (namevent != value)
                {
                    namevent = value;
                    OnPropertyChanged(nameof(NameEvent));
                }
            }
        }

        public ObservableCollection<Musico> Musicos
        {
            get => musicos;
            set
            {
                musicos = value;
                OnPropertyChanged(nameof(Musicos));
                HasMusicians = musicos != null && musicos.Count > 0; // Atualiza HasEvent
            }
        }

        public ObservableCollection<string> EventPhoto
        {
            get => eventPhoto;
            set
            {
                eventPhoto = value;
                OnPropertyChanged(nameof(EventPhoto));
                OnPropertyChanged(nameof(TotalPhotos));
                OnPropertyChanged(nameof(PhotoCounter));
            }
        }

        public int ActualPosition
        {
            get => actualPosition;
            set
            {
                if (actualPosition != value)
                {
                    actualPosition = value;
                    OnPropertyChanged(nameof(ActualPosition));
                    OnPropertyChanged(nameof(PhotoCounter));
                }
            }
        }

        public int TotalPhotos => EventPhoto?.Count ?? 0;

        public string PhotoCounter => $"{ActualPosition + 1}/{TotalPhotos}";

        public async Task EventsIsEmpyty()
        {
            HasMusicians = Musicos != null && Musicos.Count > 0;
        }

        public async Task<ObservableCollection<Musico>> ExibirTodosMusicos(long musicoDesejadoId)
        {
            try
            {
                // 1. Carrega todos os músicos da API
                ObservableCollection<Musico> musicos = await _mService.GetAllMusicians();

                // Verifica se a lista não está nula e não está vazia
                if (musicos == null || !musicos.Any())
                {
                    Musicos = new ObservableCollection<Musico>();
                    return Musicos;
                }

                // 2. Encontra o músico específico pelo ID
                Musico musicoParaDestacar = musicos
                    .FirstOrDefault(m => m.idMusico == musicoDesejadoId); // Assumindo que o campo ID se chama 'id'

                // 3. Se o músico desejado foi encontrado
                if (musicoParaDestacar != null)
                {
                    // Remove o músico da posição atual
                    musicos.Remove(musicoParaDestacar);

                    // Insere o músico na primeira posição (índice 0)
                    musicos.Insert(0, musicoParaDestacar);
                }

                // 4. Atribui a lista modificada e retorna
                Musicos = musicos;
                return Musicos;

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro ao carregar musicos: {ex.Message}");
                // Se houver erro, retorna uma lista vazia
                return Musicos = new ObservableCollection<Musico>();
            }
        }

        public async Task ChangeEventPhoto()
        {
            EventPhoto = new ObservableCollection<string>
        {
            "panelao.png",
            "birthday.png",
            "bar.png"
        };
        }





        public ICommand ClickMusicianCommand { get; set; }

        private async Task MusicianTapped(Musico musico)
        {

            if (musico == null)
                return;

            long id = musico.idMusico;

            


        }





    }
    }

