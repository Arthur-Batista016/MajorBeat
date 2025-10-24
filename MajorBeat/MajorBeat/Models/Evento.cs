using System.Collections.ObjectModel;
using MajorBeat.Models.Enums;

namespace MajorBeat.Models
{
    public class Evento
    {
        public long IdEvento { get; set; }

        public string Nome { get; set; }

        public TipoMusico TipoMusico { get; set; }
        public StatusEvento Status { get; set; }
        public TipoEvento TipoEvento { get; set; }

        public DateTime Data { get; set; }

        public string Endereco { get; set; }

        public ObservableCollection<byte[]> ImagemLocalEvento { get; set; } = new();

        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFim { get; set; }

        public string? Descricao { get; set; }
        public string Titulo { get; set; }
        public ObservableCollection<NomeInstrumento> NomeInstrumento { get; set; } = new();

        public ObservableCollection<NomeGenero> NomeGenero { get; set; } = new();

        public Musico Musico { get; set; }

        public Contratante Contratante { get; set; }

        public ObservableCollection<Avaliacao> Avaliacoes { get; set; } = new();
    }
}
