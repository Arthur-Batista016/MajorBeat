using MajorBeat.Enums;
using MajorBeat.ModelsJeff; 
using MajorBeat.Services;
using MajorBeat.Services.Usuarios;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Windows.Input;

namespace MajorBeat.ViewModels.Hirer;

public class CreateEventPageViewModel : BaseViewModel
{
    private readonly MediaService _mediaService;
    public ICommand RemoverMediaCommand { get; }
    public ICommand SelecionarMediaCommand { get; }
    public ObservableCollection<MediaFile> ArquivosDeMediaSelecionados { get; set; }
    public bool MostrarListaDeMedia => ArquivosDeMediaSelecionados.Count > 0;

    // Propriedade para MOSTRAR o placeholder (o 'Border')
    public bool MostrarPlaceholder => ArquivosDeMediaSelecionados.Count == 0;

    private async Task SelecionarMedia()
        {
            try
            {
                var pickOptions = new PickOptions
                {
                    PickerTitle = "Selecione Somente Imagens",
                    FileTypes = FilePickerFileType.Images // <-- O FILTRO
                };

                var results = await FilePicker.Default.PickMultipleAsync(pickOptions);
                if (results != null)
                {
                    foreach (var file in results)
                    {
                        // Usa o Modelo "Ideal" (MediaFile)
                        var mediaFile = new MediaFile
                        {
                            OriginalFile = file
                        };
                        ArquivosDeMediaSelecionados.Add(mediaFile);
                    }
                }
            onPropertyChanged(nameof(MostrarListaDeMedia));
            onPropertyChanged(nameof(MostrarPlaceholder));



        }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erro", $"Falha ao selecionar mídia: {ex.Message}", "OK");
            }
        }


        // O método RemoverMedia não muda
        private void RemoverMedia(MediaFile mediaFile)
        {
            if (mediaFile != null)
            {
                ArquivosDeMediaSelecionados.Remove(mediaFile);
            onPropertyChanged(nameof(MostrarListaDeMedia));
            onPropertyChanged(nameof(MostrarPlaceholder));
        }
        }




    private UsuarioService uService;
    public CreateEventPageViewModel()
    {
        _mediaService = new MediaService();
        RemoverMediaCommand = new Command<MediaFile>(RemoverMedia);


        // --- INICIALIZAÇÃO ---
        ArquivosDeMediaSelecionados = new ObservableCollection<MediaFile>();
        SelecionarMediaCommand = new Command(async () => await SelecionarMedia());

        TodosGeneros = Enum.GetValues(typeof(NomeGenero)).Cast<NomeGenero>().ToList();
        GenerosFiltrados = new ObservableCollection<NomeGenero>(TodosGeneros);
        TodosInstrumentos = Enum.GetValues(typeof(NomeInstrumento)).Cast<NomeInstrumento>().ToList();
        InstrumentosFiltrados = new ObservableCollection<NomeInstrumento>(TodosInstrumentos);


        MinDate = DateTime.Today;

        // Define a data máxima como um valor fixo (ex: daqui a 1 ano)
        MaxDate = DateTime.Today.AddYears(1);

        string token = Preferences.Get("UsuarioToken",string.Empty);
        uService = new UsuarioService(token);
        


       RegistrarCommand = new Command(async () => await EventSave());
        AddPhotoCommand = new Command(async () => await OnAddPhotoClicked());
    }

    public List<NomeGenero> TodosGeneros { get; }
    public Evento evento { get; set; } 
    public List<NomeInstrumento> TodosInstrumentos { get; }
    public ObservableCollection<NomeInstrumento> InstrumentosSelecionados { get; set; } = new();
    public ObservableCollection<NomeGenero> GenerosSelecionados { get; set; } = new();
    public ImageSource FotoSelecionada { get; set; }
    public ICommand AddPhotoCommand { get; }
    public ICommand RegistrarCommand { get; }
    public IEnumerable<TipoEvento> Tipos => Enum.GetValues(typeof(TipoEvento)).Cast<TipoEvento>();

    private DateTime dataDoEvento;
    public DateTime DataDoEvento
    {
        get => dataDoEvento;
        set
        {
            if (dataDoEvento != value)
            {
                dataDoEvento = value;
                onPropertyChanged();
            }
        }
    }

    private TimeSpan horaInicio;
    public TimeSpan HoraInicio
    {
        get => horaInicio;
        set
        {
            if (horaInicio != value)
            {
                horaInicio = value;
                onPropertyChanged();
            }
        }
    }

    private TimeSpan horaFim;
    public TimeSpan HoraFim
    {
        get => horaFim;
        set
        {
            if (horaFim != value)
            {
                horaFim = value;
                onPropertyChanged();
            }
        }
    }


    public DateTime MinDate { get; }
    public DateTime MaxDate { get; }

    private ObservableCollection<NomeInstrumento> _instrumentosFiltrados;
    public ObservableCollection<NomeInstrumento> InstrumentosFiltrados
    {
        get => _instrumentosFiltrados;
        set
        {
            _instrumentosFiltrados = value;
            onPropertyChanged(nameof(InstrumentosFiltrados));
        }
    }

    private ObservableCollection<NomeGenero> _generosFiltrados;
    public ObservableCollection<NomeGenero> GenerosFiltrados
    {
        get => _generosFiltrados;
        set
        {
            _generosFiltrados = value;
            onPropertyChanged(nameof(GenerosFiltrados));
        }
    }
    private TipoEvento _tipo;
    public TipoEvento Tipo
    {
        get => _tipo;
        set
        {
            if (_tipo != value)
            {
                _tipo = value;
                onPropertyChanged(nameof(Tipo));       // atualiza o Picker
            }
        }
    }

    private TipoMusico _tipoMusico;
    public TipoMusico TipoMusico
    {
        get => _tipoMusico;
        set
        {
            if (_tipoMusico != value)
            {
                _tipoMusico = value;
                onPropertyChanged(nameof(TipoMusico));       // atualiza o Picker
            }
        }
    }

    private byte[] _fotoBytes;
    public byte[] FotoBytes
    {
        get => _fotoBytes;
        set
        {
            _fotoBytes = value;
            onPropertyChanged(nameof(FotoBytes));
        }
    }

    private string _textoBusca;
    public string TextoBusca
    {
        get => _textoBusca;
        set
        {
            if (_textoBusca != value)
            {
                _textoBusca = value;
                onPropertyChanged(nameof(TextoBusca));
                FiltrarGeneros();
            }
        }
    }
    private string _textoBuscaInstrumento;
    public string TextoBuscaInstrumento
    {
        get => _textoBuscaInstrumento;
        set
        {
            if (_textoBuscaInstrumento != value)
            {
                _textoBuscaInstrumento = value;
                onPropertyChanged(nameof(TextoBuscaInstrumento));
                FiltrarInstrumentos();
            }
        }
    }

    private string cep = string.Empty;
    public string Cep
    {
        get { return cep; }
        set
        {
            cep = value;
            onPropertyChanged();
        }
    }

    private string numero = string.Empty;
    public string Numero
    {
        get { return numero; }
        set
        {
            numero = value;
            onPropertyChanged();
        }
    }

    private string complemento = string.Empty;
    public string Complemento
    {
        get { return complemento; }
        set
        {
            complemento = value;
            onPropertyChanged();
        }
    }




    private string nome;
    public string Nome
    {
        get => nome;
        set
        {
            if (nome != value)
            {
                nome = value;
                onPropertyChanged();
            }
        }
    }

    private string endereco;
    public string Endereco
    {
        get => endereco;
        set
        {
            if (endereco != value)
            {
                endereco = value;
                onPropertyChanged();
            }
        }
    }

    private string descricao;
    public string Descricao
    {
        get => descricao;
        set
        {
            if (descricao != value)
            {
                descricao = value;
                onPropertyChanged();
            }
        }
    }
     public async Task EventSave()
     {
         try
         {
             if (!ValidarCampos())
             {
                 await Application.Current.MainPage.DisplayAlert("Erro", "Por favor, corrija os erros nos campos destacados.", "OK");
                 return;
             }

            var token = Preferences.Get("UsuarioToken", string.Empty);
            List<string> urlsSalvas;

                 // 1. FAZ O UPLOAD (Somente se houver arquivos)
                 if (ArquivosDeMediaSelecionados.Count > 0)
                 {
                     // Chama o serviço "Ideal" UMA VEZ com a lista inteira
                     urlsSalvas = await _mediaService.UploadVariosArquivosAsync(
                         ArquivosDeMediaSelecionados,
                         token,
                         "Eventos/uploadTempMultiEvento"); // <-- O endpoint "burro" de multi-upload
                 }
                 else
                 {
                     urlsSalvas = new List<string>(); // Lista vazia
                 }



             Evento e = new Evento();


                 e.nome = Nome;
                 e.endereco = $"{Cep}, {Numero}, {Complemento}";
                 e.descricao = Descricao;
                 e.data = DataDoEvento;
                 e.instrumentos = InstrumentosSelecionados.ToList();
                 e.generos = GenerosSelecionados.ToList();
             e.imagemLocalEvento = FotoBytes;
                 e.tipoEvento = Tipo;
                 e.tipoMusico = TipoMusico;
                 e.HoraInicio = HoraInicio;
                 e.HoraFim = HoraFim;
                 e.mediaUrl = urlsSalvas;

             var service = new UsuarioService();
             var musicoCadastrado = await uService.PostEventoAsync(e);

             await Application.Current.MainPage.DisplayAlert("Aviso", "Evento criado com sucesso!!", "ok");


            await Application.Current.MainPage.Navigation.PushAsync(new Views.Hirers.HirerHomePage());

        }
         catch (Exception ex)
         {
             await Application.Current.MainPage.DisplayAlert("Erro", $"Não foi possível salvar o evento: {ex.Message}", "OK");
         }
     }
    private async Task OnAddPhotoClicked()
    {
        await Task.Yield(); // Libera o UI thread
        await Task.Delay(100);
        string action = await Application.Current.MainPage.DisplayActionSheet(
            "Adicionar Foto", "Cancelar", null, "Escolher da Galeria", "Tirar Foto");

        FileResult photo = null;

        try
        {
            if (action == "Escolher da Galeria")
            {
                photo = await MediaPicker.PickPhotoAsync();
            }
            else if (action == "Tirar Foto")
            {
                photo = await MediaPicker.CapturePhotoAsync();
            }

            if (photo != null)
            {
                using var originalStream = await photo.OpenReadAsync();

                // Copia para memória para reutilizar
                using var memoryStream = new MemoryStream();
                await originalStream.CopyToAsync(memoryStream);
                FotoBytes = memoryStream.ToArray();

                // Cria nova cópia do stream para a imagem
                FotoSelecionada = ImageSource.FromStream(() => new MemoryStream(FotoBytes));
                onPropertyChanged(nameof(FotoSelecionada));
            }
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert(
                "Erro", $"Não foi possível obter a imagem: {ex.Message}", "OK");
        }
    }

    private void FiltrarInstrumentos()
    {
        if (string.IsNullOrWhiteSpace(TextoBuscaInstrumento))
        {
            InstrumentosFiltrados = new ObservableCollection<NomeInstrumento>(TodosInstrumentos);
        }
        else
        {
            var filtro = TextoBuscaInstrumento.ToLowerInvariant();
            var filtrados = TodosInstrumentos
                .Where(i => i.ToString().ToLowerInvariant().Contains(filtro))
                .ToList();

            InstrumentosFiltrados = new ObservableCollection<NomeInstrumento>(filtrados);
        }
    }
    private void FiltrarGeneros()
    {
        var filtro = _textoBusca?.ToLower() ?? "";

        var listaFiltrada = TodosGeneros
            .Where(g => g.ToString().ToLower().Contains(filtro))
            .ToList();

        GenerosFiltrados.Clear();
        foreach (var item in listaFiltrada)
            GenerosFiltrados.Add(item);
    }

    private bool erroInstrumentoVisible;
    public bool ErroInstrumentoVisible
    {
        get => erroInstrumentoVisible;
        set { erroInstrumentoVisible = value; onPropertyChanged(); }
    }

    private bool erroTipoEventoVisible;
    public bool ErroTipoEventoVisible
    {
        get => erroTipoEventoVisible;
        set { erroTipoEventoVisible = value; onPropertyChanged(); }
    }



    private bool erroGeneroVisible;
    public bool ErroGeneroVisible
    {
        get => erroGeneroVisible;
        set { erroGeneroVisible = value; onPropertyChanged(); }
    }

    private bool erroNumeroVisible;
    public bool ErroNumeroVisible
    {
        get => erroNumeroVisible;
        set { erroNumeroVisible = value; onPropertyChanged(); }
    }

    private bool erroHorarioVisible;
    public bool ErroHorarioVisible
    {
        get => erroHorarioVisible;
        set { erroHorarioVisible = value; onPropertyChanged(); }
    }

    private bool erroTituloVisible;
    public bool ErroTituloVisible
    {
        get => erroTituloVisible;
        set { erroTituloVisible = value; onPropertyChanged(); }
    }

    private bool erroDataVisible;
    public bool ErroDataVisible
    {
        get => erroDataVisible;
        set { erroDataVisible = value; onPropertyChanged(); }
    }

    private bool erroCepVisible;
    public bool ErroCepVisible
    {
        get => erroCepVisible;
        set { erroCepVisible = value; onPropertyChanged(); }
    }
    public bool ValidarCampos()
    {
        bool isValid = true;

        // 1. Resetar todos os erros para FALSE antes de começar
        ErroTituloVisible = false;
        ErroDataVisible = false;
        ErroHorarioVisible = false;
        ErroCepVisible = false;
        // ... (Resetar todas as outras 4 propriedades de erro aqui) ...


        // --- VALIDAÇÃO DE CAMPO DE TEXTO (Título) ---
        // Checa se o Título é nulo, vazio ou tem apenas espaços em branco
        if (string.IsNullOrWhiteSpace(Nome))
        {
            ErroTituloVisible = true;
            isValid = false;
        }
        if (string.IsNullOrWhiteSpace(Numero) || !Numero.All(char.IsDigit))
        {
            ErroNumeroVisible = true;
            isValid = false;
        }
        // --- VALIDAÇÃO DE DATA ---
        // Checa se a data é a inicial padrão (ex: 01/01/0001) ou não é válida para o evento
        if (dataDoEvento == default(DateTime) || dataDoEvento < DateTime.Today)
        {
            ErroDataVisible = true;
            isValid = false;
        }

        // --- VALIDAÇÃO DE CEP/ENDEREÇO ---
        // Checa se o endereço (ou CEP, dependendo da sua propriedade) está vazio.
        // Assumindo que Endereco é onde o CEP/Endereço está armazenado.
        if (string.IsNullOrWhiteSpace(Cep) || Cep.Length < 8) // Exemplo: CEP deve ter no mínimo 8 dígitos
        {
            ErroCepVisible = true;
            isValid = false;
        }

        // --- VALIDAÇÃO DE HORÁRIO ---
        // Checa a validade da relação entre os horários (Hora Fim > Hora Início)
        if (HoraFim <= HoraInicio)
        {
            ErroHorarioVisible = true;
            isValid = false;
        }

        // --- VALIDAÇÃO DE OUTRAS PROPRIEDADES DE LISTA (Instrumentos, Gêneros) ---
        // Verifica se a lista foi selecionada ou se está vazia
        if (InstrumentosSelecionados == null || InstrumentosSelecionados.Count == 0)
        {
            ErroInstrumentoVisible = true;
            isValid = false;
        }

        if (GenerosSelecionados == null || GenerosSelecionados.Count == 0)
        {
            ErroGeneroVisible = true;
            isValid = false;
        }

        // ... Adicione aqui a lógica de validação para os campos que ativam ErroTipoEventoVisible e ErroNumeroVisible ...


        return isValid;
    }
}

